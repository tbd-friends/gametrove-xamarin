using System.Linq;
using System.Security.Claims;
using Gametrove.Core.Model;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    public enum InvitationStatus
    {
        Rejected = 0,
        Accepted
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcceptInvitationPage : ContentPage
    {
        private readonly AcceptInvitationViewModel _vm;

        public InvitationStatus Status => _vm.Status;

        public AcceptInvitationPage(AuthenticationResult result)
        {
            InitializeComponent();

            BindingContext = _vm =
                new AcceptInvitationViewModel(result.UserClaims.Single(claim => claim.Type == "email").Value)
                {
                    Navigation = Navigation
                };
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}