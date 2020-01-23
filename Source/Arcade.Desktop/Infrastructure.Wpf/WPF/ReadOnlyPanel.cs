using DevExpress.Xpf.Editors;
using System;
using System.ComponentModel;
using System.Windows;

namespace Infrastructure
{
    public class ReadOnlyPanel
    {
        public static object GetAttachedPropertyValue(DependencyObject obj, string dpName) => TypeDescriptor.GetProperties(obj, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) })[dpName]?.GetValue(obj);

        public static readonly DependencyProperty AlwaysReadOnlyProperty =
            DependencyProperty.RegisterAttached(
                "AlwaysReadOnly", typeof(bool), typeof(ReadOnlyPanel),
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits, AlwaysReadOnlyPropertyChanged));


        public static bool GetAlwaysReadOnly(DependencyObject o) => (bool)o.GetValue(AlwaysReadOnlyProperty);

        public static void SetAlwaysReadOnly(DependencyObject o, bool value)
        {
            o.SetValue(AlwaysReadOnlyProperty, value);
        }

        private static void AlwaysReadOnlyPropertyChanged(
            DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

            if (o is BaseEdit)
            {
                ((BaseEdit)o).IsReadOnly = true;
            }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.RegisterAttached(
                "IsReadOnly", typeof(bool), typeof(ReadOnlyPanel),
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits, ReadOnlyPropertyChanged));

        public static bool GetIsReadOnly(DependencyObject o) => (bool)o.GetValue(IsReadOnlyProperty);

        public static void SetIsReadOnly(DependencyObject o, bool value)
        {
            o.SetValue(IsReadOnlyProperty, value);
        }

        private static void ReadOnlyPropertyChanged(
            DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is BaseEdit)
            {
                object alwaysReadOnlyVal = GetAttachedPropertyValue(o, "ReadOnlyPanel.AlwaysReadOnly");
                bool ignore = false;
                if (alwaysReadOnlyVal is bool)
                    ignore = (bool)alwaysReadOnlyVal;
                if (!ignore)
                    ((BaseEdit)o).IsReadOnly = (bool)e.NewValue;
            }
        }
    }
}
