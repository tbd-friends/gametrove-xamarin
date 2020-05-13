using System;
using System.ComponentModel;
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
        private readonly GameDetailViewModel _vm;

        public GameDetailPage(GameDetailViewModel detailViewModel)
        {
            InitializeComponent();

            BindingContext = _vm = detailViewModel;

            MessagingCenter.Subscribe<EditGameViewModel, GameModel>(this, "Game:Updated", (vm, game) =>
            {
                detailViewModel.Name = game.Name;
                detailViewModel.Description = game.Description;
            });
        }

        public async void EditGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new EditGamePage(_vm.Id));
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

            if (_vm.Images.Count == 0)
                _vm.IsBusy = true;
        }
    }
}