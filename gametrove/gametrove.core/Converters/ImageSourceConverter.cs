using System;
using System.Globalization;
using System.IO;
using System.Net;
using Gametrove.Core.Infrastructure;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        private static readonly WebClient Client = new WebClient();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = $"{AppSettings.Configuration.Api.Url}/{value}?size=medium";

            var byteArray = Client.DownloadData(url);

            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}