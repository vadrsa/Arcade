﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Infrastructure.Mvvm
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = (value is bool && (bool)value);

            if (parameter != null && Boolean.Parse(parameter.ToString()))
                boolean = !boolean;

            return boolean ? Visibility.Visible : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
