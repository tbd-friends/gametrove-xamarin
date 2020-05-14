using System;
using System.Globalization;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class LeftRightToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string comparison)
            {
                return ((string)value).Equals(comparison, StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool @checked)
            {
                if (parameter is string current)
                {
                    return @checked ? current :
                        current.Equals("Left", StringComparison.InvariantCultureIgnoreCase) ? "Right" : "Left";
                }
            }

            throw new InvalidOperationException("You used this incorrectly");
        }
    }
}