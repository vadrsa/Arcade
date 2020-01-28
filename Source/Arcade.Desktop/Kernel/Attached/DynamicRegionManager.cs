using Kernel.Prism;
using System.Linq;
using System.Windows;

namespace Kernel
{
    public static class DynamicRegionManager
    {

        public static DynamicRegionNames GetDynamicRegionNames(DependencyObject obj)
        {
            return (DynamicRegionNames)obj.GetValue(DynamicRegionNamesProperty);
        }
        public static void SetDynamicRegionNames(DependencyObject obj, DynamicRegionNames value)
        {
            obj.SetValue(DynamicRegionNamesProperty, value);
        }

        public static readonly DependencyProperty DynamicRegionNamesProperty =
            DependencyProperty.RegisterAttached("DynamicRegionNames", typeof(DynamicRegionNames), typeof(DynamicRegionManager), new PropertyMetadata(null, OnDynamicRegionNamesChanged));

        private static void OnDynamicRegionNamesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateRegionNames(d, d);
        }

        private static void UpdateRegionNames(DependencyObject d, DependencyObject regionHolder)
        {

            foreach (DependencyObject child in LogicalTreeHelper.GetChildren(d).OfType<DependencyObject>())
            {
                string region = DynamicRegionManager.GetScreenRegion(child);
                if (region != null)
                {
                    UpdateRegionName(child, region, GetRegionNamesRecursive(regionHolder));
                }
                else
                    UpdateRegionNames(child, regionHolder);
            }
        }

        private static void UpdateRegionName(DependencyObject d, string region, DynamicRegionNames dynamicRegionNames)
        {
            if (dynamicRegionNames != null)
                Kernel.RegionManager.SetRegionName(d, dynamicRegionNames.GetName(region));
        }

        public static string GetScreenRegion(DependencyObject obj)
        {
            return (string)obj.GetValue(ScreenRegionProperty);
        }

        public static void SetScreenRegion(DependencyObject obj, string value)
        {
            obj.SetValue(ScreenRegionProperty, value);
        }

        public static readonly DependencyProperty ScreenRegionProperty =
            DependencyProperty.RegisterAttached("ScreenRegion", typeof(string), typeof(DynamicRegionManager), new PropertyMetadata(null, OnScreenRegionChanged));

        private static void OnScreenRegionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateRegionName(d, (string)e.NewValue, GetRegionNamesRecursive(d));
        }

        private static DynamicRegionNames GetRegionNamesRecursive(DependencyObject child)
        {
            DependencyObject parentObject = LogicalTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }
            var regionNames = DynamicRegionManager.GetDynamicRegionNames(parentObject);
            if (regionNames != null)
            {
                return regionNames;
            }
            else
            {
                return GetRegionNamesRecursive(parentObject);
            }
        }
    }
}
