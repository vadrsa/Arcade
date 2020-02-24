using Infrastructure.Mvvm;
using System;

namespace Arcade.ViewModels
{
    public class SessionUploadViewModel : EditableViewModel<SessionUploadViewModel>
    {
        private string _computerId;
        public string ComputerId
        {
            get => _computerId;
            set => Set(nameof(ComputerId), ref _computerId, value);
        }

        private int _Duration;
        public int Duration
        {
            get => _Duration;
            set
            {
                Set(nameof(Duration), ref _Duration, value);
                RaisePropertyChanged(nameof(PaymentDue));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => Set(nameof(StartDate), ref _startDate, value);
        }

        private ComputerTypeViewModel _type;
        public ComputerTypeViewModel Type
        {
            get => _type;
            set
            {
                Set(nameof(Type), ref _type, value);
                RaisePropertyChanged(nameof(PaymentDue));
            }
        }


        public double PaymentDue
        {
            get
            {
                if (Type == null) return 0;
                float res = Type.HourlyRate * Duration / 60;
                return Math.Ceiling(res * 100) / 100;
            }
        }

        private GameViewModel _game;
        public GameViewModel Game
        {
            get => _game;
            set => Set(nameof(Game), ref _game, value);
        }
    }

    public class SessionViewModel : EditableViewModel<SessionUploadViewModel>
    {
        private string _Id;
        public string Id
        {
            get => _Id;
            set => Set(nameof(Id), ref _Id, value);
        }

        private string _computerId;
        public string ComputerId
        {
            get => _computerId;
            set => Set(nameof(ComputerId), ref _computerId, value);
        }

        private int _Duration;
        public int Duration
        {
            get => _Duration;
            set => Set(nameof(Duration), ref _Duration, value);
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => Set(nameof(StartDate), ref _startDate, value);
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get => _EndDate;
            set => Set(nameof(EndDate), ref _EndDate, value);
        }

        private string _PaymentId;
        public string PaymentId
        {
            get => _PaymentId;
            set => Set(nameof(PaymentId), ref _PaymentId, value);
        }

        int _QueueNumber;
        public int QueueNumber
        {
            get => _QueueNumber;
            set => Set(nameof(QueueNumber), ref _QueueNumber, value);
        }
    }
}
