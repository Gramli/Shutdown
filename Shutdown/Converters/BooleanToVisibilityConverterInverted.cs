using System;
using System.Globalization;
using System.Windows;

namespace Shutdown.Converters
{
    public class BooleanToVisibilityConverterInverted : BooleanToVisibilityConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)base.Convert(value, targetType, parameter, culture) == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
