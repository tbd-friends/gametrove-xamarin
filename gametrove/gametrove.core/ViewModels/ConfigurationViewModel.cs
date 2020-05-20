using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        private Theme _selectedTheme;
        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (value != _selectedTheme)
                {
                    _selectedTheme = value;

                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<ThemeSelection> Themes { get; }

        public ConfigurationViewModel()
        {
            _scanButtonOrientation = Preferences.Get(AppPreferences.ScanButtonOrientation, "Right");

            Themes = new ObservableCollection<ThemeSelection>(new[]
            {
                new ThemeSelection {Name = "Default", Value = Theme.Default },
                new ThemeSelection {Name = "Super Nintendo", Value = Theme.SuperNintendo},
                new ThemeSelection {Name = "Junicus", Value=Theme.Junicus }
            });

            _selectedTheme =
                (Theme)Enum.Parse(typeof(Theme), Preferences.Get(AppPreferences.ApplicationTheme, "Default"));
        }

        public void UpdatePreferences()
        {
            Preferences.Set(AppPreferences.ScanButtonOrientation, _scanButtonOrientation);
            Preferences.Set(AppPreferences.ApplicationTheme, Enum.GetName(typeof(Theme), _selectedTheme));

            MessagingCenter.Send(this, "Preferences:Changed");
        }
    }

    public class ThemeSelection
    {
        public Theme Value { get; set; }
        public string Name { get; set; }
    }
}