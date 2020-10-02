using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Gametrove.Core.Services;
using Gametrove.Core.Services.Actions;
using Gametrove.Core.Services.Models;
using Xamarin.Forms;

namespace Gametrove.Core.Views.ViewModels
{
    public class StatisticsViewModel
    {
        private APIActionService _api;

        public ObservableCollection<PlatformStatistic> Statistics { get; }

        public Command LoadStatisticsCommand { get; }

        public StatisticsViewModel()
        {
            _api = DependencyService.Get<APIActionService>();

            LoadStatisticsCommand = new Command(LoadStatistics);

            Statistics = new ObservableCollection<PlatformStatistic>();
        }

        private void LoadStatistics()
        {
            Task.Run(async () =>
            {
                var statistics = await _api.Execute(new GetPlatformStatisticsAction());

                Statistics.Clear();

                foreach (var statistic in statistics)
                {
                    Statistics.Add(statistic);
                }
            });
        }
    }
}
