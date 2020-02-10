using Arcade.Management.ViewModels;
using Arcade.StaffModule.Workitems.StaffManagement.Services;
using AutoMapper;
using Infrastructure.ObjectManagment;
using Infrastructure.Utility;
using Prism.Ioc;
using Kernel.Workitems;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Arcade.StaffModule.Workitems.StaffManagement.Views
{
    public partial class StaffManagerViewModel : ObjectManagerViewModel<EmployeeViewModel, EmployeeViewModel, string>
    {
        IMapper Mapper;
        IContextService CurrentContextService;

        public StaffManagerViewModel(IContextService currentContextService, IMapper mapper, IContainerExtension container) : base(container)
        {
            CurrentContextService = currentContextService;
            Mapper = mapper;
            Initialize();
        }

        IObjectManagementService<EmployeeViewModel, EmployeeViewModel, string> objectManagementService;
        protected override IObjectManagementService<EmployeeViewModel, EmployeeViewModel, string> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = Container.Resolve<StaffManagerOMService>();
                return objectManagementService;
            }
        }


        protected override EmployeeViewModel CreateCopyDetails()
        {
            return new EmployeeViewModel()
            {
            };
        }
    }
}