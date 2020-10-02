using Gametrove.Core.Services.Models;

namespace Gametrove.Core.Views.GameDetails.ViewModels
{
    public class GameDetailMainViewModel
    {
        public GameModel GameModel { get; }

        public GameDetailMainViewModel(GameModel model)
        {
            GameModel = model;
        }
    }
}