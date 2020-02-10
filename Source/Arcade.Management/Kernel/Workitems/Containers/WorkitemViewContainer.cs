using Kernel.Prism;
using Prism.Ioc;
using Prism.Regions;
using System;

namespace Kernel.Workitems
{
    class WorkitemViewContainer : WorkitemWpfViewContainerBase, IViewContainer, IDisposable
    {

        internal WorkitemViewContainer(IWorkItem workItem, IRegionManagerExtension regionManager, IContainerExtension container) : base(workItem, regionManager, container)
        {
        }

        protected override void UnregisterView(object view, string region)
        {
            base.UnregisterView(view, region);
            RegionManager.Regions[region.ToString()].Remove(view);
        }

        protected override object RegisterView(object view, string region)
        {
            view = base.RegisterView(view, region);
            view = RegionManager.AddToRegion(region.ToString(), view);
            RegionManager.Regions[region.ToString()].Activate(view);
            return view;
        }

    }
}
