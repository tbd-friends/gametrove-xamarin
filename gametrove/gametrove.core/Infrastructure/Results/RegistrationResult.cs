using Gametrove.Core.Services.Models;

namespace Gametrove.Core.Infrastructure.Results
{
    public class RegistrationResult
    {
        public GameModel Model { get; set; }
        public bool ShouldScan { get; set; }
    }
}