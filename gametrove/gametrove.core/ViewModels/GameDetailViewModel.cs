using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class GameDetailViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public DateTime Registered { get; set; }

        private readonly APIService _api;

        public ObservableCollection<GameImage> Images { get; }
        public Command LoadImagesCommand { get; }

        public GameDetailViewModel(GameModel source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            Platform = source.Platform;
            Registered = source.RegisteredDate;
            Images = new ObservableCollection<GameImage>();
            LoadImagesCommand = new Command(async () => await LoadImages());

            _api = DependencyService.Get<APIService>();
        }

        public async Task UploadImageForGame(Stream content)
        {
            await _api.UploadImageForGame(Id, content, $"{Name}_{DateTime.UtcNow:s}.jpg");
        }

        public async Task LoadImages()
        {
            IsBusy = true;

            var images = await _api.GetImagesForGame(Id);

            Images.Clear();

            foreach (var image in images)
            {
                Images.Add(new GameImage { Url = image });
            }

            IsBusy = false;
        }

        public class GameImage
        {
            public string Url { get; set; }
        }
    }
}