using Infrastructure.ChangeTracking;
using System;
using System.Windows.Data;

namespace Infrastructure.Mvvm
{
    /// <summary>
    /// Shows If EditMode is in not editing state
    /// </summary>
    [ValueConversion(typeof(EditMode), typeof(bool))]
    public class EditModeBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            // Assure targetType is bool
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a bool");

            return ((EditMode)value) == EditMode.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
