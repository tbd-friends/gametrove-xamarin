using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Scandit.BarcodePicker.Unified;
using Scandit.BarcodePicker.Unified.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();

            ScanditService.ScanditLicense.AppKey = AppSettings.Configuration.Scandit;
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterGamePage());
        }

        private async void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as CollectionView).SelectedItem as GameModel;

            await Navigation.PushAsync(new GameDetailPage(new GameViewModel(selectedItem)), true);
        }

        private async void ScanCode_Clicked(object sender, EventArgs e)
        {
            await CheckIfICanUseTheCamera();

            // Configure the barcode picker through a scan settings instance by defining which
            // symbologies should be enabled.
            var settings = ScanditService.BarcodePicker.GetDefaultScanSettings();

            // prefer backward facing camera over front-facing cameras.
            settings.CameraPositionPreference = CameraPosition.Back;

            // Enable symbologies that you want to scan.
            settings.EnableSymbology(Symbology.Ean13);
            settings.EnableSymbology(Symbology.Upca);
            settings.EnableSymbology(Symbology.Qr);

            ScanditService.BarcodePicker.DidScan += BarcodePickerOnDidScan;

            await ScanditService.BarcodePicker.ApplySettingsAsync(settings);

            // Start the scanning process.
            await ScanditService.BarcodePicker.StartScanningAsync();
        }

        private void BarcodePickerOnDidScan(ScanSession session)
        {
            ScanditService.BarcodePicker.DidScan -= BarcodePickerOnDidScan;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await ScanditService.BarcodePicker.StopScanningAsync();

                if (session.AllRecognizedCodes.Any())
                {
                    string scannedCode = session.AllRecognizedCodes.Last().Data;

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