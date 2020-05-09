using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Gametrove.Core.ViewModels.Results;
using Syncfusion.ListView.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Gametrove.Core.Views
{
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        private ZXingScannerPage _scanner;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();

            MessagingCenter.Subscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered",
                async (sender, result) =>
                {
                    if (result.ShouldScan) await StartScanning();
                });
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterGamePage());
        }

        private async void SfListView_OnSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            var selectedItem = (sender as SfListView).SelectedItem as GameModel;

            await Navigation.PushAsync(new GameDetailPage(new GameViewModel(selectedItem)), true);
        }

        private async void ScanCode_Clicked(object sender, EventArgs e)
        {
            await StartScanning();
        }

        private async Task StartScanning()
        {
            await CheckIfICanUseTheCamera();

            _scanner = new ZXingScannerPage();

            _scanner.OnScanResult += BarcodePickerOnDidScan;

            await Navigation.PushModalAsync(_scanner);
        }

        private void BarcodePickerOnDidScan(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                _scanner.IsScanning = false;
                _scanner.OnScanResult -= BarcodePickerOnDidScan;

                await Navigation.PopModalAsync();

                if (!string.IsNullOrEmpty(result.Text))
                {
                    string scannedCode = result.Text;

                    var game = await _viewModel.LoadGameByCode(scannedCode);

                    if (game == null)
                    {
                        await Navigation.PushAsync(
                            new RegisterGamePage(new RegisterGameViewModel(scannedCode)), true);
                    }
                    else
                    {
                        await Navigation.PushAsync(new GameDetailPage(new GameViewModel(game)), true);
                    }
                }
            });
        }

        private async Task<PermissionStatus> CheckIfICanUseTheCamera()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            return status;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Games.Count == 0)
                _viewModel.IsBusy = true;
        }
    }
}