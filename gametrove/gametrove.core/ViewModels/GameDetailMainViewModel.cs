using Gametrove.Core.Services.Models;

namespace Gametrove.Core.ViewModels
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