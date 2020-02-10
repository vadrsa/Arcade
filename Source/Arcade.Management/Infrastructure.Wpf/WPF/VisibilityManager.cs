using DevExpress.Xpf.Ribbon;
using Infrastructure.Resources;
using System;
using System.Linq;
using System.Windows;

namespace Infrastructure
{
    public static class VisibilityManager
    {

        // TODO: keep track of subscribed pages and add new pages on merge
        public static bool GetRibbonVisibleIfEmpty(DependencyObject obj)
        {
            return (bool)obj.GetValue(RibbonVisibleIfEmptyProperty);
        }

        public static void SetRibbonVisibleIfEmpty(DependencyObject obj, bool value)
        {
            obj.SetValue(RibbonVisibleIfEmptyProperty, value);
        }

        // Using a DependencyProperty as the backing store for RibbonVisibleIfEmpty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RibbonVisibleIfEmptyProperty =
            DependencyProperty.RegisterAttached("RibbonVisibleIfEmpty", typeof(bool), typeof(VisibilityManager), new PropertyMetadata(true, OnRibbonVisibleIfEmptyChanged));

        private static void OnRibbonVisibleIfEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RibbonControl ribbon = d as RibbonControl;
            if (ribbon != null)
            {
                bool ribbonVisibleIfEmptyProperty = (bool)e.NewValue;
                if (ribbonVisibleIfEmptyProperty == false)
                {
                    DetermineRibbonVisibility(ribbon);
                    ribbon.Items.CollectionChanged += (o, ev) => Items_CollectionChanged(ribbon, ev);
                    ribbon.SelectedPageChanged += (o, ev) =>
                    {
                        DetermineRibbonVisibility(ribbon);
                    };

                    foreach (var item in ribbon.Items.OfType<WorkitemRibbonPageCategory>())
                    {
                        item.VisibilityChanged += Item_VisibilityChanged;
                    }
                }

            }
        }

        private static void Items_CollectionChanged(RibbonControl sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<WorkitemRibbonPageCategory>())
                {
                    item.VisibilityChanged += Item_VisibilityChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<WorkitemRibbonPageCategory>())
                {
                    item.VisibilityChanged -= Item_VisibilityChanged;
                }
            }

            DetermineRibbonVisibility(sender);
        }

        private static void Item_VisibilityChanged(object sender, EventArgs e)
        {
            RibbonPageCategory category = (RibbonPageCategory)sender;
            DetermineRibbonVisibility(category.Ribbon);
        }

        private static void DetermineRibbonVisibility(RibbonControl ribbon)
        {
            var list = ribbon.ActualCategories.Where(i => i is WorkitemRibbonPageCategory && ((WorkitemRibbonPageCategory)i).IsVisible == true);
            if (list.Count() == 0)
                ribbon.Visibility = Visibility.Collapsed;
            else
                ribbon.Visibility = Visibility.Visible;
        }
    }
}
