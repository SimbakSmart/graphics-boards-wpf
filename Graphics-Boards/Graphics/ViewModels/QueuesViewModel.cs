

using CommunityToolkit.Mvvm.ComponentModel;
using Core.Models;
using Infraestructure.Helpers;
using Infraestructure.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System.Diagnostics;


namespace Graphics.ViewModels
{
    public partial class QueuesViewModel : ObservableObject
    {
        private QueueServices qs = null;


        #region BAR GRAPH RESPONSALES 
        [ObservableProperty]
        private SeriesCollection _queueCollection;
        [ObservableProperty]
        public string[] _queueLabels;
        [ObservableProperty]
        public Func<double, string> _queueFormatter;
        [ObservableProperty]
        private List<Queues> _listQueues = null;
        #endregion


        public QueuesViewModel()
        {
            qs = new QueueServices();
            //Task.Run(async () => await StartLoadGraphsAsync());
            StartLoadGraphsAsync().Wait();
        }

        // METHODS
        public async Task StartLoadGraphsAsync()
        {
            try
            {
                await BarGraphByResponsableAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
               await qs.DisposeAsync();
            }
        }

        private async Task BarGraphByResponsableAsync(FiltersParams filters = null)
        {
            ListQueues?.Clear();

            ListQueues = await qs.GetTotalsByResponsableAsync();

            string[] _queuesArray = ListQueues.Select(q => q.Name).ToArray();

            QueueCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<int> (  ListQueues.Select(q => q.Total).ToArray())
                }
            };

            QueueLabels = _queuesArray;
            QueueFormatter = value => value.ToString("N");

        }

    }
}
