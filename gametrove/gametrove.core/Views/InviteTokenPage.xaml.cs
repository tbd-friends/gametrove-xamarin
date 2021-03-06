﻿using Gametrove.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InviteTokenPage : ContentPage
    {
        private readonly InviteTokenViewModel _vm;

        public InviteTokenPage()
        {
            InitializeComponent();

            BindingContext = _vm = new InviteTokenViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.GetCurrentInviteTokenCommand.Execute(null);
        }
    }
}