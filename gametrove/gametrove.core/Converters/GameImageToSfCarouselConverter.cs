using System;
using System.Collections.Generic;
using System.Globalization;
using Gametrove.Core.ViewModels;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;

namespace Gametrove.Core.Converters
{
    public class GameImageToSfCarouselConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<GameDetailViewModel.GameImage> source)
            {
                var results = new List<SfCarouselItem>();

                foreach (var item in source)
                {
                    results.Add(new SfCarouselItem { ImageName = item.Url });
                }

                return results;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}