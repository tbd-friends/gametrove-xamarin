using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Gametrove.Core.Infrastructure;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Xamarin.Forms;

namespace Gametrove.Core.ViewModels
{
    public class RegisterCopyViewModel : BaseViewModel
    {
        public Guid Id { get; private set; }
        public ObservableCollection<string> Tags { get; private set; }

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

        public Command RegisterCopyCommand { get; private set; }

        private readonly APIActionService _api;

        public RegisterCopyViewModel(Guid id)
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
            Task.Run(() => _api.Execute(new RegisterGameCopyAction(Id, Tags.ToArray(), Cost, Purchased)));
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }
    }
}