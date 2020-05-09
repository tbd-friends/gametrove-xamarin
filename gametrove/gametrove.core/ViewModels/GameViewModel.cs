using System;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;

namespace Gametrove.Core.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public string Registered { get; set; }

        public GameViewModel(GameModel source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
        }
    }
}