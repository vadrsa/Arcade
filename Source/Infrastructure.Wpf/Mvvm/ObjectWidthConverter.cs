using System;
using System.Globalization;
using System.Windows.Data;

namespace Infrastructure.Mvvm
{
    public class ObjectWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null) ? 0 : Double.NaN;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
