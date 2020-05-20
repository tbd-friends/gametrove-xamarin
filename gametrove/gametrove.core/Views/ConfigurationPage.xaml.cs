using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Resources.Themes;
using Gametrove.Core.ViewModels;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.Themes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage
    {
        private readonly ConfigurationViewModel _vm;

        public ConfigurationPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ConfigurationViewModel();
        }

        private void ToggleButton_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            _vm.UpdatePreferences();
        }

        private void SfComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is SfComboBox source &&
                source.SelectedValue is string selectedTheme)
            {
                _vm.SelectedTheme = (Theme)Enum.Parse(typeof(Theme), selectedTheme);

                _vm.UpdatePreferences();

                Application.Current.Resources.SetCurrentTheme();
            }
        }
    }
}