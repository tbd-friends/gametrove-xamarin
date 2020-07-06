using Gametrove.Core.ViewModels;

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
