using System;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameDetailMainPage : TabbedPage
    {
        private readonly bool _showCopies;
        private readonly GameDetailMainViewModel _vm;

        public GameDetailMainPage(GameModel game, bool showCopies = false)
        {
            _showCopies = showCopies;
            InitializeComponent();

            BindingContext = _vm = new GameDetailMainViewModel(game);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CurrentPage = _showCopies ? Children[1] : CurrentPage;
        }

        public async void EditTitle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditTitlePage(_vm.GameModel.Id));
        }
    }
}