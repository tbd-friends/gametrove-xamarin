using System.Collections.Generic;
using System.Threading.Tasks;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class GetRecentlyAddedGamesAction : IApiAction<IEnumerable<GameSearchModel>>
    {
        public Task<IEnumerable<GameSearchModel>> DoAsync(APIActionService service)
        {
            return service.Execute(new SearchForGameAction(string.Empty, 10));
        }
    }
}