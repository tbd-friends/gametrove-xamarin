using System;
using System.Globalization;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class ScanButtonPositionToRectangleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string position)
            {
                switch (position.ToLower())
                {
                    case "left":
                        return new Rectangle(0, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize);
                    case "right":
                    default:
                        return new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize);
                }
            }

            return new Rectangle(0, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}