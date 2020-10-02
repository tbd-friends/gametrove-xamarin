using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Infrastructure.Cache;
using Gametrove.Core.Services;
using Gametrove.Core.Views;
using Gametrove.Core.Views.Login;
using Syncfusion.Licensing;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<APIActionService>();
            DependencyService.Register<GenreLookup>();
            DependencyService.Register<UserAuthentication>();
            DependencyService.Register<RecentGamesList>();
            DependencyService.Register<IConfirmationService, ConfirmationService>();

            SyncfusionLicenseProvider.RegisterLicense(AppSettings.Configuration.Syncfusion);

            Resources.SetCurrentTheme();

            MainPage = new LoginPage();
        }

        protected override async void OnStart()
        {
            await CheckIfICanUseTheCamera();

            await CheckIfICanUseTheInternet();
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {
            await CheckIfICanUseTheCamera();

            await CheckIfICanUseTheInternet();
        }


        private async Task CheckIfICanUseTheInternet()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.NetworkState>();

            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.NetworkState>();
            }
        }

        private async Task CheckIfICanUseTheCamera()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();
            }
        }
    }
}
