using AnaliticsModule.Workitems.ComputerIncome;
using Infrastructure.Prism;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class AnaliticsModule : Module
    {

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            CurrentContextService.LaunchModalWorkItem<ComputerIncomeWorkitem>();
        }
    }
}
