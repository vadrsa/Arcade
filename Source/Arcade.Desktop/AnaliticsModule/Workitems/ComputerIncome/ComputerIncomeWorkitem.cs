using AnaliticsModule.Workitems.ComputerIncome.Views;
using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaliticsModule.Workitems.ComputerIncome
{
    class ComputerIncomeWorkitem : WorkitemWpfBase
    {
        public ComputerIncomeWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Computer Incomes";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new ComputerIncomeView(), KnownRegions.Content);
        }
    }
}
