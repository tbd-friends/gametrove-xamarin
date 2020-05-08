﻿using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Syncfusion.Licensing;
using Xamarin.Forms;

namespace Gametrove.Core
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<APIService>();

            SyncfusionLicenseProvider.RegisterLicense(AppSettings.Configuration.Syncfusion);

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}