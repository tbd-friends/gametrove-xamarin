using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class AddCopyViewModel : BaseViewModel
    {
        public Guid Id { get; }
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

        private bool _isWanted;

        public bool IsWanted
        {
            get => _isWanted;
            set
            {
                if (_isWanted != value)
                {
                    _isWanted = value;

                    OnPropertyChanged();
                }
            }

        }
        public Command RegisterCopyCommand { get; private set; }

        private readonly APIActionService _api;

        public AddCopyViewModel(Guid id)
        {
            Id = id;

            Tags = new ObservableCollection<string>();

            RegisterCopyCommand = new Command(() =>
            {
                RegisterCopy();

                MessagingCenter.Send(this, "Copy:Added");
            });

            _api = DependencyService.Get<APIActionService>();
        }

        private void RegisterCopy()
        {
            Task.Run(() => _api.Execute(
                new AddGameCopyAction(Id,
                    new CopyModel
                    {
                        Cost = Cost,
                        Tags = Tags,
                        IsWanted = IsWanted,
                        Purchased = Purchased
                    })));
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }
    }
}