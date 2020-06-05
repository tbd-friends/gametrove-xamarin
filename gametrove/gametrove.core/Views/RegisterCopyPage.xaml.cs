using System;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCopyPage : ContentPage
    {
        private readonly RegisterCopyViewModel _vm;

        public RegisterCopyPage(Guid id)
        {
            InitializeComponent();

            BindingContext = _vm = new RegisterCopyViewModel(id);
        }

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
            if (sender is Entry entry && !string.IsNullOrEmpty(entry.Text))
            {
                _vm.AddTag(entry.Text);

                entry.Text = string.Empty;
                entry.Focus();
            }
        }
    }
}