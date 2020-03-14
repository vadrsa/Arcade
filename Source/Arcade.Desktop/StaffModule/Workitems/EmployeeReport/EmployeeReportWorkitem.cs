using Infrastructure.Constants;
using Kernel;
using Kernel.Workitems;
using Prism.Ioc;
using StaffModule.Workitems.EmployeeReport.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModule.Workitems.EmployeeReport
{
    public class EmployeeReportWorkitem : WorkitemBase, ISupportsInitialization<EmployeeReportWorkitemInitializer>
    {
        private EmployeeReportWorkitemInitializer initializer;

        public EmployeeReportWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Employee Report";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = container.Register<EmployeeReportView>(new EmployeeReportView(), KnownRegions.Content);
            var viewModel = (EmployeeReportViewModel)view.DataContext;
            viewModel.Load(initializer.Id, initializer.Date);
        }

        public void Initialize(EmployeeReportWorkitemInitializer initializer)
        {
            this.initializer = initializer;
        }
    }
}
