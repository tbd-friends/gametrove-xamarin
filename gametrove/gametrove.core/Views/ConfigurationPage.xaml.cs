using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}