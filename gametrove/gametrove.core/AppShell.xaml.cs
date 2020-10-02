using Gametrove.Core.Views.ViewModels;

namespace Gametrove.Core
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = new AppShellViewModel();
        }
    }
}
