using DevExpress.Xpf.LayoutControl;
using System.Windows;

namespace Infrastructure
{
    public class DataProperties
    {
        // Using a DependencyProperty as the backing store for Required.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequiredProperty =
            DependencyProperty.RegisterAttached("Required", typeof(bool), typeof(DataProperties), new PropertyMetadata(false, OnRequiredChanged));

        private static void OnRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayoutItem)
            {
                LayoutItem item = (LayoutItem)d;
                if ((bool)e.NewValue == true && (bool)e.OldValue == false)
                    item.Label += "*";
                if ((bool)e.NewValue == false && (bool)e.OldValue == true)
                    item.Label = item.Label.ToString().Substring(0, item.Label.ToString().Length - 1);
            }
        }

        public static void SetRequired(DependencyObject element, bool value)
        {
            element.SetValue(RequiredProperty, value);
        }
        public static bool GetRequired(DependencyObject element)
        {
            return (bool)element.GetValue(RequiredProperty);
        }

    }
}
