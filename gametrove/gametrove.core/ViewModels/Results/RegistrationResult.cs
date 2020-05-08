using Gametrove.Core.Services.Models;

namespace Gametrove.Core.ViewModels.Results
{
    public class RegistrationResult
    {
        public GameModel Model { get; set; }
        public bool ShouldScan { get; set; }
    }
}