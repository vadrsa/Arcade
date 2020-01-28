using Kernel.Prism;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Windows;

namespace Kernel.Workitems
{
    class ModalWorkitemViewContainer : WorkitemWpfViewContainerBase, IViewContainer, IDisposable
    {
        DynamicRegionNames RegionNames;

        internal ModalWorkitemViewContainer(IWorkItem workItem, IRegionManagerExtension regionManager, IContainerExtension container) : base(workItem, regionManager, container)
        {
            RegionNames = DynamicRegionManager.GetDynamicRegionNames(WorkItem.Window.GetRegionHolder());
        }

        protected override void UnregisterView(object view, string region)
        {
            base.UnregisterView(view, region);
            RegionManager.Regions[RegionNames.GetName(region)].Remove(view);
        }

        protected override object RegisterView(object view, string region)
        {
            view = base.RegisterView(view, region);
            view = RegionManager.AddToRegion(RegionNames.GetName(region), view);
            RegionManager.Regions[RegionNames.GetName(region)].Activate(view);
            return view;
        }

    }
}
