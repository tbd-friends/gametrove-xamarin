using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Gametrove.Core.ViewModels.Results;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Gametrove.Core.Views
{
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        private readonly ZXingScannerPage _scanner;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();

            _scanner = new ZXingScannerPage();
            _scanner.OnScanResult += BarcodePickerOnDidScan;

            MessagingCenter.Unsubscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered");
            MessagingCenter.Subscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered",
                async (sender, result) =>
                {
                    await Navigation.PopAsync(true);

                    if (result.ShouldScan)
                    {
                        await StartScanning();
                    }
                    else
                    {
                        await Navigation.PushAsync(new GameDetailMainPage(result.Model, showCopies: true), true);
                    }
                });
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterGamePage());
        }

        private async void SfListView_OnSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            if (!(sender is SfListView listView))
                return;

            var selectedItem = listView.SelectedItem as GameModel;

            await Navigation.PushAsync(new GameDetailMainPage(selectedItem), true);

            listView.SelectedItem = null;
        }

        private async void ScanCode_Clicked(object sender, EventArgs e)
        {
            await StartScanning();
        }

        private async Task StartScanning()
        {
            await Navigation.PushModalAsync(_scanner);
        }

        private void BarcodePickerOnDidScan(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                _scanner.IsScanning = false;

                await Navigation.PopModalAsync();

                if (string.IsNullOrEmpty(result.Text))
                    return;

                string scannedCode = result.Text;

                var game = await _viewModel.LoadGameByCode(scannedCode);

                if (game == null)
                {
                    await Navigation.PushAsync(
                        new RegisterGamePage(new RegisterGameViewModel(scannedCode)), true);
                }
                else
                {
                    await Navigation.PushAsync(new GameDetailMainPage(game), true);
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.IsBusy = true;
        }
    }
}