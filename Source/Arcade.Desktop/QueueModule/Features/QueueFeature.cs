using Kernel.Configuration;
using QueueModule.Workitems.QueueDisplay;
using QueueModule.Workitems.QueueStart;
using QueueModule.Workitems.QueueStart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueModule.Features
{
    public class QueueFeature : Feature
    {
        public async override void Attach()
        {
            base.Attach();
            await CurrentContextService.LaunchModalWorkItem<QueueStartWorkitem>();
        }
    }
}
