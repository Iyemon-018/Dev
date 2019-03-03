namespace LiveCharts.Wpf.Example
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            List<WorkTime> workTimes = new List<WorkTime>
                            {
                                new WorkTime
                                {
                                    Date       = DateTime.Today.AddDays(-3)
                                  , ActualTime = 900
                                }
                              , new WorkTime
                                {
                                    Date       = DateTime.Today.AddDays(-3)
                                  , ActualTime = 7200
                                }
                              , new WorkTime
                                {
                                    Date       = DateTime.Today.AddDays(-2)
                                  , ActualTime = 1800
                                }
                              , new WorkTime
                                {
                                    Date       = DateTime.Today.AddDays(-2)
                                  , ActualTime = 6000
                                }
                              , new WorkTime
                                {
                                    Date       = DateTime.Today.AddDays(-2)
                                  , ActualTime = 7200
                                }
                            };

            Series = new SeriesCollection();
            Series.AddRange(workTimes.GroupBy(x => x.Date)
                                     .Select(x => new PieSeries
                                                  {
                                                      Title = x.Key.ToString("yyyy/MM/dd")
                                                    , Values = new ChartValues<int>(new[]
                                                                                    {
                                                                                        x.Sum(v => v.ActualTime)
                                                                                    })
                                                    , DataLabels = true
                                                    , LabelPoint = p => $"{p.SeriesView.Title}{Environment.NewLine} {p.Participation:P}"
                                                    , FontSize = 18.0d
                                                  }));

            SeriesColors = new ColorsCollection();
            SeriesColors.AddRange(new[] {"#FFE65100", "#FFEF6C00", "#FFF57C00", "#FFFB8C00", "#FFFF9800"}
                                  .Select(System.Windows.Media.ColorConverter.ConvertFromString)
                                  .OfType<System.Windows.Media.Color>()
                                  .ToList());
        }

        public SeriesCollection Series { get; }

        public ColorsCollection SeriesColors { get; }
    }
}