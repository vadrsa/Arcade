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
            if (d is Window)
                UpdateRegionNames(d, (Window)d);

        }

        private static void UpdateRegionNames(DependencyObject d, Window window)
        {

            foreach (DependencyObject child in LogicalTreeHelper.GetChildren(d).OfType<DependencyObject>())
            {
                string region = DynamicRegionManager.GetScreenRegion(child);
                if (region != null)
                {
                    UpdateRegionName(child, region, window);
                }
                else
                    UpdateRegionNames(child, window);
            }
        }

        private static void UpdateRegionName(DependencyObject d, string region, Window window)
        {

            DynamicRegionNames dynamicRegionNames = DynamicRegionManager.GetDynamicRegionNames(window);
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
            Window window = GetParentWindow(d);
            if (window != null)
            {
                UpdateRegionName(d, (string)e.NewValue, window);
            }
        }

        /// <summary>
        /// Gets the parent window of a dependency object.
        /// TODO: Check implementation
        /// http://www.viblend.com/forum/d_postst154_Find-the-parent-Window-of-a-WPF-control.aspx
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        private static Window GetParentWindow(DependencyObject child)
        {
            DependencyObject parentObject = LogicalTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            Window parent = parentObject as Window;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return GetParentWindow(parentObject);
            }
        }
    }
}
