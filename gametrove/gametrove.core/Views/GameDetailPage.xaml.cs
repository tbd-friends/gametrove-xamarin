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

            MessagingCenter.Subscribe<EditTitleViewModel, TitleModel>(this, "Title:Updated", (vm, title) =>
            {
                detailViewModel.Name = title.Name;
                detailViewModel.Subtitle = title.Subtitle;
            });

            MessagingCenter.Subscribe<RegisterCopyViewModel>(this, "Copy:Added", _ =>
            {
                Navigation.PopAsync(true);

                _vm.LoadCopiesCommand.Execute(this);
            });
        }

        public async void EditTitle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new EditTitlePage(_vm.Id));
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_vm.Images.Count == 0)
            {
                await _vm.LoadImages();
            }

            await _vm.LoadCopies();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterCopyPage(_vm.Id));
        }
    }
}