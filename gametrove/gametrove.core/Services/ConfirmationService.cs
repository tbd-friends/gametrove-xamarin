using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gametrove.Core.Services
{
    public class ConfirmationService : IConfirmationService
    {
        public async Task<bool> Confirm(string message)
        {
            return await Application.Current.MainPage.DisplayAlert("Please confirm", message, "Yes", "No");
        }
    }
}