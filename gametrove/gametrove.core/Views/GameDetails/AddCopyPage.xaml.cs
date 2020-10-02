using System;
using Gametrove.Core.Views.GameDetails.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views.GameDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCopyPage : ContentPage
    {
        private readonly AddCopyViewModel _vm;

        public AddCopyPage(Guid id)
        {
            InitializeComponent();

            BindingContext = _vm = new AddCopyViewModel(id);
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