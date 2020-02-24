using Arcade.ViewModels;
using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.ObjectManagement;
using Infrastructure.Utils;
using Infrastructure.Workitems;
using Kernel;
using Kernel.Configuration;
using Kernel.Workitems;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using SecurityModule.Services;
using SharedEntities;
using StaffModule.Services;
using StaffModule.Views;
using StaffModule.Workitems.SessionManagerDetails.Views;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StaffModule.Workitems.SessionManagerDetails
{
    public class SessionManagerDetailsWorkitem : WorkitemWpfBase, ISupportsInitialization<ObjectManagerDetailsInitializer<SessionUploadViewModel>>
    {
        public override string WorkItemName => "Session Manager Details";
        private Project project;

        private ObjectManagerDetailsInitializer<SessionUploadViewModel> _data;
        private SessionDetailsViewModel viewModel;
        protected SessionDetailsViewModel ViewModel => viewModel;


        public SessionManagerDetailsWorkitem(IContainerExtension container) : base(container)
        {
            this.project = container.Resolve<Project>();
        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(Infrastructure.Constants.Commands.SaveObject, SaveCommand);
        }

        public void Initialize(ObjectManagerDetailsInitializer<SessionUploadViewModel> data)
        {
            _data = data;

        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = container.Register<SessionDetailsView>(Container.Resolve<SessionDetailsView>(), KnownRegions.Content);
            viewModel = (SessionDetailsViewModel)view.DataContext;
            viewModel.Details = _data.Details;
            viewModel.IsAdding = _data.IsAdding;
            IsAdding = _data.IsAdding;
        }

        public bool IsAdding { get; set; }

        private AsyncCommand _saveCommand;

        public AsyncCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new AsyncCommand(Save, CanSave);
                return _saveCommand;
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private async Task Save()
        {
            await viewModel.LoadCustom(DoSave);
        }

        protected virtual async Task DoSave(CancellationToken token)
        {
            if (IsAdding)
            {
                await DoCreate(token);
                Communication.OnNext(new WorkitemEventArgs(this, true));
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                await DoEnqueue(token);
                Communication.OnNext(new WorkitemEventArgs(this, true));
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        protected virtual async Task DoCreate(CancellationToken token)
        {
            await new SessionService().Create(Mapper.Map<SessionUploadDto>(ViewModel.Details));
        }

        protected virtual async Task DoEnqueue(CancellationToken token)
        {
            var session = await new SessionService().Enqueue(Mapper.Map<SessionUploadDto>(ViewModel.Details));
            var computer = Mapper.Map<ComputerViewModel>(session.Computer);
            if (session.QueueNumber > 0)
            {
                System.Windows.Size size = default;
                try
                {
                    var setting = await new SystemSettingService().GetById((int)SystemSettingType.QueuePrintDimensions);
                    var dimensions = setting.Value.Split('x');
                    size = new System.Windows.Size(Int32.Parse(dimensions[0]), Int32.Parse(dimensions[1]));
                }
                catch(Exception ex)
                {
                    throw new Exception("Failed to print. Check QueuePrintDimensions system setting it should be in a '___x___' format");
                }

                var check = new QueueCheck();
                check.DataContext = new QueueCheckViewModel { Computer = computer.ComputerFullName, QueueNumber = session.QueueNumber, Game = ViewModel.Details?.Game?.Name };
                check.Print(size);

            }
        }
    }
}

