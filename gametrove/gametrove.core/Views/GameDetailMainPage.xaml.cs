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
        private GameDetailMainViewModel _vm;

        public GameDetailMainPage(GameModel game)
        {
            InitializeComponent();

            BindingContext = _vm = new GameDetailMainViewModel(game);
        }

        public async void EditTitle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditTitlePage(_vm.GameModel.Id));
        }
    }
}