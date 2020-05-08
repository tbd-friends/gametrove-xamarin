﻿using System;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Models;
using Gametrove.Core.ViewModels;
using Gametrove.Core.ViewModels.Results;
using Syncfusion.SfAutoComplete.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs;

namespace Gametrove.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterGamePage : ContentPage
    {
        private RegisterGameViewModel _vm;

        public RegisterGamePage() : this(new RegisterGameViewModel())
        { }

        public RegisterGamePage(RegisterGameViewModel vm)
        {
            InitializeComponent();

            BindingContext = _vm = vm;

            MessagingCenter.Subscribe<RegisterGameViewModel, RegistrationResult>(this, "Game:Registered",
                async (sender, result) => { await Navigation.PopAsync(true); });
        }

        protected override void OnAppearing()
        {
            _vm.GetPlatformsCommand.Execute(this);
        }

        private void SfAutoComplete_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as SfAutoComplete).SelectedItem is PlatformModel selectedValue)
            {
                _vm.Platform = selectedValue.Id;
            }
        }
    }
}