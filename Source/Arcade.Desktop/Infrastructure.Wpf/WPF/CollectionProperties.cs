using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Infrastructure
{
    public static class CollectionProperties
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
            DependencyProperty.RegisterAttached("HideIfEmpty", typeof(bool), typeof(CollectionProperties), new PropertyMetadata(false, OnHideEmptyChanged));

        private static void OnHideEmptyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true && d is ItemsControl)
            {
                var itemsControl = (ItemsControl)d;
                ((INotifyCollectionChanged)itemsControl.Items).CollectionChanged += (o, ev) => HideIfEmpty(itemsControl);
            }
        }

        private static void HideIfEmpty(ItemsControl control)
        {
            if (control.Items.Count == 0)
                control.Visibility = Visibility.Collapsed;
            else
                control.Visibility = Visibility.Visible;
        }
    }
}
