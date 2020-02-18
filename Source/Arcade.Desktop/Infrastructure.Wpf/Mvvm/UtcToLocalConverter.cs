using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Infrastructure.Mvvm
{
    public class UtcToLocalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = ((DateTime)value);
            if (date == DateTime.MinValue) return DateTime.MinValue;
            var newDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            return newDate.ToLocalTime();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToUniversalTime();
        }
    }
}
