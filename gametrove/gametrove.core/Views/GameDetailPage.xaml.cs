using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Gametrove.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class GameDetailPage : ContentPage
    {
        private GameDetailViewModel _vm;

        public static readonly BindableProperty ModelProperty = BindableProperty.Create(
            nameof(Model),
            typeof(GameModel),
            typeof(GameDetailPage),
            default(GameModel), propertyChanged: (bindable, oldvalue, newvalue) =>
              {
                  if (bindable is GameDetailPage page &&
                      newvalue is GameModel model)
                  {
                      page.Model = model;
                  }
              });

        public GameModel Model
        {
            get => (GameModel)BindingContext;
            set => BindingContext = _vm = new GameDetailViewModel(value);
        }

        public GameDetailPage()
        {
            InitializeComponent();

            MessagingCenter.Unsubscribe<EditTitleViewModel, TitleModel>(this, "Title:Updated");
            MessagingCenter.Subscribe<EditTitleViewModel, TitleModel>(this, "Title:Updated", (vm, title) =>
            {
                _vm.Name = title.Name;
                _vm.Subtitle = title.Subtitle;
            });
        }

        public async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available", "OK");

                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "my_images"
            });

            await _vm.UploadImageForGame(file.GetStreamWithImageRotatedForExternalStorage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.LoadImagesCommand.Execute(null);
        }
    }
}