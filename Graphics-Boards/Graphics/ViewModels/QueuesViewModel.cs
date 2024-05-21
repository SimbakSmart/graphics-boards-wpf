

using CommunityToolkit.Mvvm.ComponentModel;
using Core.Models;
using Infraestructure.Helpers;
using Infraestructure.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Diagnostics;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Axis = LiveChartsCore.SkiaSharpView.Axis;
using LiveChartsCore.Measure;


namespace Graphics.ViewModels
{
    public partial class QueuesViewModel : ObservableObject
    {
        private QueueServices qs = null;



        #region BAR GRAPH RESPONSALES 
        [ObservableProperty]
        private ColumnSeries<double> _queueColumSeries;

        [ObservableProperty]
        private List<Queues> _listQueue= null;

        [ObservableProperty]
        public ISeries[] _queueSeries;

        [ObservableProperty]
        public Axis[] _queueAxis;
        #endregion


        #region  TABLE RANGE DAYS 
        [ObservableProperty]
        private List<Queues> _listByRange;
        #endregion

        public QueuesViewModel()
        {
            qs = new QueueServices();
            Task.Run(async () => await StartLoadGraphsAsync());
        }

        // METHODS
        public async Task StartLoadGraphsAsync()
        {
            try
            {
                await BarGraphByResponsableAsync();
                await GetTotalsByRangeAsync();
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
            ListQueue?.Clear();
            ListQueue = await qs.GetTotalsByResponsableAsync();

            QueueColumSeries= new ColumnSeries<double>()
            {
                Name="Reportes En Queue",
                Values= ListQueue.Select(q=>(double)q.Total).ToList(),
                Padding=0.5,
                MaxBarWidth=70.0,
               // MaxBarWidth=double.PositiveInfinity,
                Fill = new SolidColorPaint(new SKColor(25, 118, 210, 255)),
            };

            Axis _axis = new Axis()
            {
                Labels = ListQueue.Select(q => q.Name).ToList(),
                TextSize = 12,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                IsVisible = true,
                LabelsRotation = -90,
                Position = AxisPosition.Start,
                Padding = new LiveChartsCore.Drawing.Padding(0),

            };
            QueueSeries = new ISeries[] { QueueColumSeries};
            QueueAxis  = new Axis[]{ _axis};

        }
        private async Task GetTotalsByRangeAsync(FiltersParams filters = null)
        {
            if (filters != null)
            {
                ListByRange = await qs.GetTotalsByRangeDaysAsync(filters);
            }
            else
            {
                ListByRange = await qs.GetTotalsByRangeDaysAsync();
            }

        }

    }
}
