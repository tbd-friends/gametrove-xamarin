using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        private StatisticsViewModel _vm;

        public StatisticsPage()
        {
            InitializeComponent();

            BindingContext = _vm = new StatisticsViewModel();
        }
    }
}