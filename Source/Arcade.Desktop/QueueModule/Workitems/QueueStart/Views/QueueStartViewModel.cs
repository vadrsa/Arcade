using Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueModule.Workitems.QueueStart.Views
{
    class QueueStartViewModel : WorkitemViewModel<QueueStartWorkitem>
    {
        protected override void OnWorkitemSet()
        {
            base.OnWorkitemSet();

            var result = Workitem.LocalStorage.Request("ComputerId");
            if (result.Exists)
                ComputerID = (string)result.Value;
        }

        public string _computerID;
        public string ComputerID
        {
            get => _computerID;
            set
            {
                Set(ref _computerID, value, nameof(ComputerID));
            }
        }


        private AsyncCommand _openCommand;

        public AsyncCommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                    _openCommand = new AsyncCommand(Open);
                return _openCommand;
            }
        }

        private async Task Open()
        {
            if (String.IsNullOrEmpty(ComputerID))
            {
                IsLoadingFaulted = true;
                LoadingErrorMessage = "Computer ID cannot be empty";
            }
            else
            {
                Workitem.LocalStorage.Write("ComputerId", ComputerID);
                if (await Workitem.Close())
                    await Workitem.DisplayQueue(ComputerID);
            }
        }
    }
}
