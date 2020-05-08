using System.ComponentModel;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;

namespace Gametrove.Core.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class GameDetailPage : ContentPage
    {
        private readonly GameViewModel _viewModel;

        public GameDetailPage(GameViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }
    }
}