using GymTrack.DatabaseRelated;
using GymTrack.DataTransferObjects;
using GymTrack.Resources.Languages;
using System.Collections.Generic;
using System.Windows;

namespace GymTrack
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsViewModel statisticsModel { get; set; }
        public StatisticswUIlanguageResources UILanguageRes { get; set; }
        public StatisticswLanguageResources TextRes { get; set; }
        private StatType currentStatisticsType = StatType.CompletionTime;

        /// <summary>
        /// Window for displaying statistics of trainings
        /// </summary>
        public StatisticsWindow()
        {
            UILanguageRes = LanguageManager.PopulateStatysticsw_UIlanguageResources();
            TextRes = LanguageManager.PopulateStatysticswlanguageResources();
            InitializeComponent();

            List<StatisticsStampDto> statObjectsList = StatisticsReader.ReadStatObjectsFromDatabase();
            if (statObjectsList.Count == 0)
            {
                StatisticsStackPanel.Visibility = Visibility.Collapsed;
                NoDataTextBlock.Text = UILanguageRes.NoDataText;
                NoDataTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                StatisticsStackPanel.Visibility = Visibility.Visible;
                NoDataTextBlock.Visibility = Visibility.Collapsed;
            }

            Title = UILanguageRes.WindowTitle;

            if (!Properties.Settings.Default.IsWeightTracked)
            {
                WeightButton.Visibility = Visibility.Collapsed;
            }

            UpdateUI();
        }
        private void UpdateUI()
        {
            statisticsModel = new StatisticsViewModel(currentStatisticsType);
            DataContext = statisticsModel;
            switch (currentStatisticsType)
            {
                case StatType.CompletionTime:
                    StatisticsHeaderTextBlock.Text = $"{UILanguageRes.HeaderFirstPart} {TextRes.CompletionTimeStatChapter}";
                    break;
                case StatType.Enforceability:
                    StatisticsHeaderTextBlock.Text = $"{UILanguageRes.HeaderFirstPart} {TextRes.EnforceabilityStatChapter}";
                    break;
                case StatType.Sets:
                    StatisticsHeaderTextBlock.Text = $"{UILanguageRes.HeaderFirstPart} {TextRes.SetsStatChapter}";
                    break;
                case StatType.Weight:
                    StatisticsHeaderTextBlock.Text = $"{UILanguageRes.HeaderFirstPart} {TextRes.WeightStatChapter}";
                    break;
                default: break;
            }

            StackpanelButtonContainer.DataContext = UILanguageRes;
            BackToMainMenuButton.DataContext = UILanguageRes;
        }

        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void CompletionTimeButton_Click(object sender, RoutedEventArgs e)
        {
            currentStatisticsType = StatType.CompletionTime;
            UpdateUI();
        }

        private void EnforceabilityButton_Click(object sender, RoutedEventArgs e)
        {
            currentStatisticsType = StatType.Enforceability;
            UpdateUI();
        }

        private void SetsButton_Click(object sender, RoutedEventArgs e)
        {
            currentStatisticsType = StatType.Sets;
            UpdateUI();
        }

        private void WeightButton_Click(object sender, RoutedEventArgs e)
        {
            currentStatisticsType = StatType.Weight;
            UpdateUI();
        }
    }
}
