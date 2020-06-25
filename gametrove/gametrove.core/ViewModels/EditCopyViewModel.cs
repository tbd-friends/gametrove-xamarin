﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class EditCopyViewModel : BaseViewModel
    {
        public ObservableCollection<string> Tags { get; }

        private decimal? _cost;
        public decimal? Cost
        {
            get => _cost;
            set
            {
                if (value != _cost)
                {
                    _cost = value;

                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _purchased;
        public DateTime? Purchased
        {
            get => _purchased;
            set
            {
                if (_purchased != value)
                {
                    _purchased = value;

                    OnPropertyChanged();
                }
            }
        }

        public Command UpdateCopyCommand { get; }
        public Command CancelCommand { get; }
        public Command AddTagCommand { get; }

        public INavigation Navigation { get; set; }

        private readonly Guid _id;
        private readonly Guid _gameId;
        private readonly APIActionService _api;

        public EditCopyViewModel(Guid gameId, CopyModel model)
        {
            _id = model.Id;
            _gameId = gameId;
            Tags = new ObservableCollection<string>(model.Tags);
            Purchased = model.Purchased;
            Cost = model.Cost;

            _api = DependencyService.Get<APIActionService>();

            UpdateCopyCommand = new Command(async () =>
            {
                await UpdateCopy();

                await Navigation.PopModalAsync(true);
            });

            CancelCommand = new Command(async () => await Navigation.PopModalAsync(true));

            AddTagCommand = new Command<string>(AddTag);
        }

        private void AddTag(string tag)
        {
            Tags.Add(tag);

            MessagingCenter.Send(this, "Tag:Added");
        }

        private async Task<CopyModel> UpdateCopy()
        {
            return await _api.Execute(new UpdateCopyAction(_gameId, new CopyModel
            {
                Id = _id, 
                Tags = Tags,
                Cost = Cost,
                Purchased = Purchased
            }));
        }
    }
}