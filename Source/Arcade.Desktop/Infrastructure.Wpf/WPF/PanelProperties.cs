using Prism;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Infrastructure
{
    public static class PanelProperties
    {
        public static bool GetHideIfEmpty(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideIfEmptyProperty);
        }

        public static void SetHideIfEmpty(DependencyObject obj, bool value)
        {
            obj.SetValue(HideIfEmptyProperty, value);
        }

        // Using a DependencyProperty as the backing store for HideIfEmpty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HideIfEmptyProperty =
            DependencyProperty.RegisterAttached("HideIfEmpty", typeof(bool), typeof(PanelProperties), new PropertyMetadata(false, OnHideIfEmptyChanged));

        private static void OnHideIfEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false) return;

            if (d is Panel)
            {
                Panel panel = ((Panel)d);
                foreach (var item in panel.Children.OfType<IActiveAware>().ToList())
                {
                    item.IsActiveChanged += (o, k) => InferVisibility(panel);
                }
                InferVisibility(panel);
            }
        }

        public static void CollectionChanged(DependencyObject d, NotifyCollectionChangedEventArgs e)
        {
            if (!GetHideIfEmpty(d)) return;
            if (d is Panel)
            {
                Panel panel = ((Panel)d);
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in e.NewItems.OfType<IActiveAware>().ToList())
                    {
                        item.IsActiveChanged += (o, k) => InferVisibility(panel);
                    }
                }

                InferVisibility(panel);
            }
        }

        private static void InferVisibility(Panel panel)
        {
            if (!GetHideIfEmpty(panel)) return;
            if (panel.Children.OfType<UIElement>().Where(c => c.Visibility == Visibility.Visible).Count() == 0)
                panel.Visibility = Visibility.Collapsed;
            else
                panel.Visibility = Visibility.Visible;
        }
    }
}
