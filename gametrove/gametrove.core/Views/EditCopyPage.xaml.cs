using System;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCopyPage : ContentPage
    {
        public EditCopyViewModel _vm;

        public EditCopyPage(Guid gameId, CopyModel model)
        {
            InitializeComponent();

            MessagingCenter.Unsubscribe<EditCopyViewModel>(this, "Tag:Added");
            MessagingCenter.Subscribe<EditCopyViewModel>(this, "Tag:Added", _ =>
            {
                TagEntry.Text = "";
                TagEntry.Focus();
            });

            BindingContext = _vm = new EditCopyViewModel(gameId, model) { Navigation = Navigation };
        }
    }
}