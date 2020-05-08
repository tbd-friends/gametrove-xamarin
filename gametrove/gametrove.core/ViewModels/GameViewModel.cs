using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;

namespace Gametrove.Core.ViewModels
{
    public class GameViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public GameViewModel(GameModel source) 
        {
            Name = source.Name;
            Description = source.Description;
        }
    }
}