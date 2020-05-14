using Gametrove.Core.Infrastructure;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
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

        public ConfigurationViewModel()
        {
            _scanButtonOrientation = Preferences.Get(AppPreferences.ScanButtonOrientation, "Right");
        }

        public void UpdatePreferences()
        {
            Preferences.Set(AppPreferences.ScanButtonOrientation, _scanButtonOrientation);

            MessagingCenter.Send(this, "Preferences:Changed");
        }
    }
}