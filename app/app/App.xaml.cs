using System;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using app.Services;
using app.Views;

namespace app
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<APIService>();

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
