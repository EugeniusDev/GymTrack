using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using GymTrack.DatabaseRelated;
using GymTrack.DataTransferObjects;
using LiveCharts;
using LiveCharts.Wpf;

namespace GymTrack
{
    /// <summary>
    /// Used for populating statistics chart
    /// </summary>
    public partial class StatisticsViewModel : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private readonly List<StatisticsStampDto> _statisticsStamps;
        private readonly List<DateTime> _dateTimes = new List<DateTime>();
        /// <summary>
        /// Constructor that populates StatisticsViewModel as needed
        /// </summary>
        /// <param name="typeOfStatisticsToGet">Type of statistics to display on a chart</param>
        public StatisticsViewModel(StatType typeOfStatisticsToGet)
        {
            _statisticsStamps = StatisticsReader.ReadStatObjectsFromDatabase();
            if (_statisticsStamps.Count == 0)
            {
                return;
            }
            // Creating list of DateTime to later use as titles of a chart
            DateTime currentIterationDateTime = _statisticsStamps[0].DateOfTrainingCompletion;
            while (!currentIterationDateTime.Date.Equals(DateTime.Today))
            {
                _dateTimes.Add(currentIterationDateTime);
                currentIterationDateTime = currentIterationDateTime.AddDays(1);
            }
            _dateTimes.Add(currentIterationDateTime);

            SeriesCollection = new SeriesCollection();
            switch (typeOfStatisticsToGet)
            {
                case StatType.CompletionTime:
                    Formatter = value => value.ToString("N2");
                    Labels = new[] { string.Empty };
                    for (int i = 0; i < _dateTimes.Count; i++) {
                        SeriesCollection.Add(new ColumnSeries
                        {
                            Title = _dateTimes[i].ToShortDateString(),
                            Values = GetTimeValues(_dateTimes[i])
                        });
                    }
                    break;
                case StatType.Enforceability:
                    Formatter = value => value.ToString("N0");
                    Labels = _dateTimes.Select(dateTime => dateTime.ToShortDateString()).ToArray();
                    for (int i = 0; i < _dateTimes.Count; i++)
                    {
                        SeriesCollection.Add(new ColumnSeries
                        {
                            Title = _dateTimes[i].ToShortDateString(),
                            Values = GetTrainingsCount(_dateTimes[i])
                        });
                    }
                    break;
                case StatType.Sets:
                    Formatter = value => value.ToString("N0");
                    Labels = _dateTimes.Select(dateTime => dateTime.ToShortDateString()).ToArray();
                    for (int i = 0; i < _dateTimes.Count; i++)
                    {
                        SeriesCollection.Add(new ColumnSeries
                        {
                            Title = _dateTimes[i].ToShortDateString(),
                            Values = GetSetsCount(_dateTimes[i])
                        });
                    }
                    break;
                case StatType.Weight:
                    Formatter = value => value.ToString("N2");
                    Labels = _dateTimes.Select(dateTime => dateTime.ToShortDateString()).ToArray();
                    for (int i = 0; i < _dateTimes.Count; i++)
                    {
                        SeriesCollection.Add(new ColumnSeries
                        {
                            Title = _dateTimes[i].ToShortDateString(),
                            Values = GetAverageWeightValues(_dateTimes[i])
                        });
                    }
                    break;
                default: break;
            }

        }
        /// <summary>
        /// Provides ChartValues of training's completion time on given date
        /// </summary>
        /// <param name="dateToSearchFor">Date when trainings could be completed</param>
        /// <returns>ChartValues based on given date; empty collection, if there are no such values</returns>
        private ChartValues<double> GetTimeValues(DateTime dateToSearchFor)
        {
            ChartValues<double> timeValues = new ChartValues<double>();
            _statisticsStamps.ForEach(sObj =>
            {
                if (sObj.DateOfTrainingCompletion.Date.Equals(dateToSearchFor.Date))
                {
                    double hourValue;
                    if (Properties.Settings.Default.IsTimeIn24HourFormat)
                    {
                        hourValue = sObj.DateOfTrainingCompletion.ToLocalTime().Hour;
                    }
                    else
                    {
                        hourValue = (sObj.DateOfTrainingCompletion.ToLocalTime().Hour >= 11) ?
                        sObj.DateOfTrainingCompletion.ToLocalTime().Hour - 12
                        : sObj.DateOfTrainingCompletion.ToLocalTime().Hour;
                    }
                    double minutesValue = sObj.DateOfTrainingCompletion.ToLocalTime().Minute / 100d;
                    timeValues.Add(hourValue + minutesValue);
                }
            });
            return timeValues;
        }
        /// <summary>
        /// Provides ChartValues of count of completed trainings on given date
        /// </summary>
        /// <param name="dateToSearchFor">Date when trainings could be completed</param>
        /// <returns>ChartValues based on given date; empty collection, if there are no such values</returns>
        private ChartValues<int> GetTrainingsCount(DateTime dateToSearchFor)
        {
            int trainingsCount = 0;
            _statisticsStamps.ForEach(sObj =>
            {
                if (sObj.DateOfTrainingCompletion.Date.Equals(dateToSearchFor.Date))
                {
                    trainingsCount++;
                }
            });

            ChartValues<int> trainingValue = new ChartValues<int> { trainingsCount };
            return trainingValue;
        }
        /// <summary>
        /// Provides ChartValues of count of sets completed in trainings on given date
        /// </summary>
        /// <param name="dateToSearchFor">Date when trainings could be completed</param>
        /// <returns>ChartValues based on given date; empty collection, if there are no such values</returns>
        private ChartValues<int> GetSetsCount(DateTime dateToSearchFor)
        {
            int setsCount = 0;
            _statisticsStamps.ForEach(sObj =>
            {
                if (sObj.DateOfTrainingCompletion.Date.Equals(dateToSearchFor.Date))
                {
                    setsCount += sObj.SetsCountSum;
                }
            });

            ChartValues<int> setsValue = new ChartValues<int> { setsCount };
            return setsValue;
        }
        /// <summary>
        /// Provides ChartValues of average weight on given date
        /// </summary>
        /// <param name="dateToSearchFor">Date when trainings could be completed</param>
        /// <returns>ChartValues based on given date; empty collection, if there are no such values</returns>
        private ChartValues<double> GetAverageWeightValues(DateTime dateToSearchFor)
        {
            try
            {
                double averageWeight = _statisticsStamps.Where(sObj => sObj.DateOfTrainingCompletion.Date.Equals(dateToSearchFor.Date))
                    .Average(sObj => sObj.Weight);
                ChartValues<double> weightValue = new ChartValues<double> { averageWeight };
                return weightValue;
            }
            catch
            {
                // Ignore empty days
            }

            return new ChartValues<double>();
        }
    }
}
