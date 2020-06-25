using System.Threading.Tasks;
using Gametrove.Core.Model;

namespace Gametrove.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> Authenticate();
        AuthenticationResult AuthenticationResult { get; }
    }
}