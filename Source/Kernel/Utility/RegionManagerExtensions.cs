using Kernel.Managers;
using Kernel.Prism;
using Kernel.Workitems;
using Prism.Regions;
using System.Linq;
using System.Windows;

namespace Kernel.Utility
{
    public static class RegionManagerExtensions
    {

        public static void RemoveWorkitemViews(this IRegionManagerExtension regionManager, IWorkItem workItem)
        {
            foreach (IRegion region in regionManager.Regions)
            {
                regionManager.RemoveWorkitemFromRegion(region, workItem);
            }
        }
        public static void DeactivateWorkitem(this IRegionManagerExtension regionManager, IWorkItem workItem)
        {
            foreach (IRegion region in regionManager.Regions.ToList())
            {
                Application.Current.Dispatcher.InvokeIfNeeded(() => regionManager.DeactivateWorkitemInRegion(region, workItem));
            }
        }

        public static void ActivateWorkitem(this IRegionManagerExtension regionManager, IWorkItem workItem)
        {
            foreach (IRegion region in regionManager.Regions.ToList())
            {
                regionManager.ActivateWorkitemInRegion(region, workItem);
            }
        }

        public static void RemoveWorkitemFromRegion(this IRegionManagerExtension regionManager, IRegion region, IWorkItem workItem)
        {
            foreach (DependencyObject view in region.Views.OfType<DependencyObject>())
            {
                IWorkItem owner = WorkitemManager.GetOwner(view);
                if (workItem.Equals(owner))
                    region.Remove(view);
            }
        }

        public static void DeactivateWorkitemInRegion(this IRegionManagerExtension regionManager, IRegion region, IWorkItem workItem)
        {

            foreach (DependencyObject view in region.Views.OfType<DependencyObject>())
            {
                IWorkItem owner = WorkitemManager.GetOwner(view);
                if (workItem.Equals(owner))
                    region.Deactivate(view);
            }
        }

        public static void ActivateWorkitemInRegion(this IRegionManagerExtension regionManager, IRegion region, IWorkItem workItem)
        {
            foreach (DependencyObject view in region.Views.OfType<DependencyObject>())
            {

                CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>()
                    .RunUIThread(() =>
                    {
                        IWorkItem owner = WorkitemManager.GetOwner(view);
                        if (workItem.Equals(owner) && region.Views.Contains(view))
                            region.Activate(view);
                    });
            }
        }

        public static void ClearNavigation(this IRegionManagerExtension regionManager, string region)
        {
            regionManager.Regions[region].RemoveAll();
            regionManager.Regions[region].NavigationService.Journal.Clear();
        }
    }
}
