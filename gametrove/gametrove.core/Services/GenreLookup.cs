using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Services.Actions;
using Xamarin.Forms;

namespace Gametrove.Core.Services
{
    public class GenreLookup
    {
        private readonly APIActionService _api;
        private bool _isValid;
        private IEnumerable<string> _genres;

        public async Task<IEnumerable<string>> GetGenres()
        {
            if (!_isValid)
            {
                _genres = await _api.Execute(new GetGenreLabelsAction());

                _isValid = true;
            }

            return _genres;
        }

        public GenreLookup()
        {
            _api = DependencyService.Get<APIActionService>();
            _isValid = false;
        }

        public void Invalidate(IEnumerable<string> provided)
        {
            _isValid = provided.Any(x => !_genres.Contains(x)) ||
                       _genres.Any(x => !provided.Contains(x));
        }
    }
}