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
            string fullUrl = $"{AppSettings.Configuration.Api.Url}/{value}";

            var byteArray = Client.DownloadData(fullUrl);

            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}