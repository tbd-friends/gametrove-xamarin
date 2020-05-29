using System;
using System.Globalization;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "0";
            }

            decimal result = (decimal)value;

            return result.ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string asString) || string.IsNullOrEmpty(asString))
            {
                return 0;
            }

            if (decimal.TryParse(asString, out decimal result))
            {
                return result;
            }

            return 0;
        }

    }
}