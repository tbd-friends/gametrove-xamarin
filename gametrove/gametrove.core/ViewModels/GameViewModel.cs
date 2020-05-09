using System;
using System.IO;
using System.Threading.Tasks;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public string Registered { get; set; }

        private APIService _api;

        public GameViewModel(GameModel source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;

            _api = DependencyService.Get<APIService>();
        }

        public async Task UploadImageForGame(Stream content)
        {
            await _api.UploadImageForGame(Id, content, $"{Name}_{DateTime.UtcNow:s}.jpg");
        }
    }
}