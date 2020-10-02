using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class CoverArtImageSourceConverter : IValueConverter
    {
        private static readonly WebClient Client = new WebClient();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if( value is IEnumerable<GameImage> images)
            {
                var coverArt = images.SingleOrDefault(i => i.IsCoverArt);

                if (coverArt != null)
                {
                    var url = $"{AppSettings.Configuration.Api.Url}/{coverArt.Url}?size=small";
                    
                    var byteArray = Client.DownloadData(url);
                    
                    return ImageSource.FromStream(() => new MemoryStream(byteArray));                    
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}