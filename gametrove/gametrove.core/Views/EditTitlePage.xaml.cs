using System;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Syncfusion.SfAutoComplete.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTitlePage : ContentPage
    {
        private readonly EditTitleViewModel _vm;

        public EditTitlePage(Guid gameId)
        {
            InitializeComponent();

            BindingContext = _vm = new EditTitleViewModel(gameId);

            MessagingCenter.Subscribe<EditTitleViewModel, TitleModel>(this, "Title:Updated", async (vm, game) =>
                {
                    await Navigation.PopModalAsync(true);
                });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _vm.Initialize();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
            if (sender is SfAutoComplete entry && !string.IsNullOrEmpty(entry.Text))
            {
                _vm.Genres.Add(entry.Text);

                entry.Text = string.Empty;
                entry.Focus();
            }
        }

        private void SfAutoComplete_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is SfAutoComplete entry && entry.SelectedIndex > -1)
            {
                _vm.Genres.Add((string)entry.SelectedItem);

                entry.Text = string.Empty;
                entry.Focus();
            }
        }
    }
}