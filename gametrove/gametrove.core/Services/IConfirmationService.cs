using System.Threading.Tasks;

namespace Gametrove.Core.Services
{
    public interface IConfirmationService
    {
        Task<bool> Confirm(string message);
    }
}