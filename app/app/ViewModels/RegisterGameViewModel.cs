using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using app.Services;
using Xamarin.Forms;

namespace app.ViewModels
{
    public class RegisterGameViewModel : INotifyPropertyChanged
    {
        private readonly APIService _service;
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (!value.Equals(_name))
                {
                    _name = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (!value.Equals(_description))
                {
                    _description = value;

                    OnPropertyChanged();
                }
            }
        }

        private string _code;

        public string Code
        {
            get => _code;
            set
            {
                if (!value.Equals(_code))
                {
                    _code = value;

                    OnPropertyChanged();
                }
            }
        }

        public Command RegisterGame { get; }

        public RegisterGameViewModel()
        {
            _service = DependencyService.Resolve<APIService>();

            RegisterGame = new Command(async () => await RegisterNewGame());
        }

        private async Task RegisterNewGame()
        {
            await _service.RegisterNewGame(Name, Description, Code);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}