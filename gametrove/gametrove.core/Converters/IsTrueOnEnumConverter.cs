using System;
using System.Globalization;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class IsTrueOnEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter != null && Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
