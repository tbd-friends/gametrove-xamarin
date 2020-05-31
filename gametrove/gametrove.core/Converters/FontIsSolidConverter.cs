using System;
using System.Globalization;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class FontIsSolidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSolid)
            {
                return isSolid
                    ? Application.Current.Resources["FontAwesomeSolidFree"]
                    : Application.Current.Resources["FontAwesomeRegularFree"];
            }

            return Application.Current.Resources["FontAwesomeSolidFree"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}