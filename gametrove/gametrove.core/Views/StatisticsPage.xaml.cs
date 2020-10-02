using Gametrove.Core.Views.ViewModels;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.LoadStatisticsCommand.Execute(null);
        }
    }
}