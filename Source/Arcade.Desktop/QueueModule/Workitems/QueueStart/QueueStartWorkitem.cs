using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using QueueModule.Workitems.QueueDisplay;
using QueueModule.Workitems.QueueStart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueModule.Workitems.QueueStart
{
    class QueueStartWorkitem : WorkitemWpfBase
    {
        public QueueStartWorkitem(IContainerExtension container) : base(container)
        {
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new QueueStartView(), KnownRegions.Content);
        }

        public override string WorkItemName => "Start Queue";

        public async Task DisplayQueue(string computerID)
        {
            await CurrentContextService.LaunchWorkItem<QueueDisplayWorkitem>(computerID);
        }
    }
}
