using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core.Views.GameDetails.ViewModels
{
    public class GameDetailViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Platform { get; set; }
        public DateTime Registered { get; set; }
        public decimal CompleteInBoxPrice { get; set; }
        public decimal LoosePrice { get; set; }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (value != _isFavorite)
                {
                    _isFavorite = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _scanButtonOrientation;

        public string ScanButtonOrientation
        {
            get => _scanButtonOrientation;
            set
            {
                if (value != _scanButtonOrientation)
                {
                    _scanButtonOrientation = value;

                    OnPropertyChanged();
                }
            }
        }

        private readonly APIActionService _api;

        public ObservableCollection<GameImage> Images { get; }
        public ObservableCollection<string> Genres { get; }

        public Command DeleteImageCommand { get; }
        public Command ToggleCoverArtCommand { get; }
        public Command ToggleFavoriteCommand { get; }

        public IConfirmationService ConfirmationService { get; }

        public GameDetailViewModel(GameModel source)
        {
            Id = source.Id;
            Name = source.Name;
            Subtitle = source.Subtitle;
            Platform = source.Platform;
            Registered = source.Registered;
            IsFavorite = source.IsFavorite;
            CompleteInBoxPrice = source.CompleteInBoxPrice ?? 0;
            LoosePrice = source.LoosePrice ?? 0;
            Images = source.Images != null
                ? new ObservableCollection<GameImage>(source.Images)
                : new ObservableCollection<GameImage>();
            Genres = source.Genres != null
                ? new ObservableCollection<string>(source.Genres)
                : new ObservableCollection<string>();

            DeleteImageCommand = new Command<string>(async (url) => await DeleteImage(url));
            ToggleCoverArtCommand = new Command<GameImage>(async (id) => await ToggleCoverArt(id));

            _scanButtonOrientation = Preferences.Get(AppPreferences.ScanButtonOrientation, "Right");

            ConfirmationService = DependencyService.Get<IConfirmationService>();

            ToggleFavoriteCommand = new Command(ToggleFavorite);

            _api = DependencyService.Get<APIActionService>();
        }

        public async Task UploadImageForGame(Stream content)
        {
            await _api.Execute(new UploadImageForGameAction(Id, content, $"{Name}_{DateTime.UtcNow:s}.jpg"));
        }

        public void ToggleFavorite()
        {
            Task.Run(async () =>
            {
                if (IsFavorite)
                {
                    await _api.Execute(new MarkGameAsNotFavoriteAction(Id));
                }
                else
                {
                    await _api.Execute(new MarkGameAsFavoriteAction(Id));
                }

                IsFavorite = !IsFavorite;
            });
        }

        public async Task DeleteImage(string url)
        {
            if (await ConfirmationService.Confirm("Are you sure you would like to delete this image?"))
            {
                await _api.Execute(new DeleteImageAction(url));
            }
        }

        public async Task ToggleCoverArt(GameImage gameImage)
        {
            string message = gameImage.IsCoverArt
                ? "Are you sure you would like to deselect the image as 'cover art'?"
                : "Are you sure you would like to make this image the 'cover art'?";

            if (await ConfirmationService.Confirm(message))
            {
                await _api.Execute(new ToggleCoverArtAction(gameImage.Id));
            }
        }
    }
}