using Infrastructure.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Arcade.ViewModels
{
    public class ComputerQueueViewModel : EditableViewModel<ComputerQueueViewModel>, IIdEntityViewModel<string>
    {

        private string _id;
        public string Id
        {
            get => _id;
            set => Set(nameof(Id), ref _id, value);
        }

        private string _typeId;
        public string TypeId
        {
            get => _typeId;
            set => Set(nameof(TypeId), ref _typeId, value);
        }

        private int _number;
        public int Number
        {
            get => _number;
            set => Set(nameof(Number), ref _number, value);
        }

        private bool _isTerminated;
        public bool IsTerminated
        {
            get => _isTerminated;
            set => Set(nameof(IsTerminated), ref _isTerminated, value);
        }

        private ComputerTypeViewModel _type;
        public ComputerTypeViewModel Type
        {
            get => _type;
            set {
                Set(nameof(Type), ref _type, value);
                TypeId = value?.Id;
            }
        }

        private SessionViewModel _current;
        public SessionViewModel Current
        {
            get => _current;
            set
            {
                Set(nameof(Current), ref _current, value);
                RaisePropertyChanged(nameof(NextAvailableTime));
                RaisePropertyChanged(nameof(PotentialProblemWithQueue));
            }
        }


        private ObservableCollection<SessionViewModel> _queue;
        public ObservableCollection<SessionViewModel> Queue
        {
            get => _queue;
            set
            {
                Set(nameof(Queue), ref _queue, value);
                RaisePropertyChanged(nameof(NextAvailableTime));
                RaisePropertyChanged(nameof(PotentialProblemWithQueue));
            }
        }

        public DateTime NextAvailableTime
        {
            get
            {
                if (Current == null) return default;
                if (Current.StartDate == default) return default;
                var nextAvailableTime = Current.StartDate.AddMinutes(Current.Duration);

                if (Queue != null && Queue.Count != 0)
                    nextAvailableTime = Queue.Aggregate(nextAvailableTime, (a, s) => a.AddMinutes(s.Duration));
                return nextAvailableTime;
            }
        }


        public bool PotentialProblemWithQueue
        {
            get
            {
                if (Current == null && (Queue != null && Queue.Count != 0))
                    return true;
                return false;
            }
        }

    }
}
