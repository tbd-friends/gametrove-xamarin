using System.Text;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services.Models;
using Newtonsoft.Json;

namespace Gametrove.Core.Services.Actions
{
    public class UpdateTitleAction : IApiAction<TitleModel>
    {
        private readonly TitleModel _model;

        public UpdateTitleAction(TitleModel model)
        {
            _model = model;
        }

        public async Task<TitleModel> DoAsync(APIActionService service)
        {
            var response = await service.Client.PutAsync($"titles/{_model.Id}",
                    _model.AsStringContent(Encoding.UTF8))
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<TitleModel>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}