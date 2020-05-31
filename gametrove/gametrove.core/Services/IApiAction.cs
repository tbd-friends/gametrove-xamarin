using System.Threading.Tasks;

namespace Gametrove.Core.Services
{
    public interface IApiAction<TResult>
    {
        Task<TResult> DoAsync(APIActionService service);
    }

    public interface IApiAction
    {
        Task DoAsync(APIActionService service);
    }
}