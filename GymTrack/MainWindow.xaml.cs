using GymTrack.DatabaseRelated;
using GymTrack.Objects;
using GymTrack.Resources.Languages;
using GymTrack.Themes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GymTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MwUIlanguageResources UILanguageRes { get; set; }
        public MwlanguageResources TextRes { get; set; }
        
        #region Configuration and startup
        /// <summary>
        /// Window that plays role of main menu
        /// </summary>
        public MainWindow()
        {
            LanguageManager.UpdateCurrentLanguageResource();
            ThemeResourcesUpdater.UpdateTheme();
            TextRes = LanguageManager.PopulateMWlanguageResources();
            UILanguageRes = LanguageManager.PopulateMW_UIlanguageResources();
            DataContext = this;

            InitializeComponent();

            // If account is not configured, it is required to configure it
            if (!Properties.Settings.Default.AccountConfigured)
            {
                AccountSettingsWindow accountSettingsWindow = new AccountSettingsWindow();
                accountSettingsWindow.Show();
                Close();
            }

            TrainingReader trainingReader = new TrainingReader();
            List<TrainingDto> existingTrainingObjects = trainingReader.GetAllTrainingObjects();
            List<TrainingMainInfoDto> trainingsMainInfo = Transformer.MapTrainingObjectsToTrainingMainInfo(existingTrainingObjects);
            // If there are created trainings
            if (trainingsMainInfo.Count != 0)
            {
                NoTrainingsYetTextBlock.Visibility = Visibility.Collapsed;
                // Populating the ListView with TrainingMainInfoDtos
                TrainingsInfoListView.ItemsSource = trainingsMainInfo;
            }
        }
        #endregion

        #region Button panel related
        private void AccountSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AccountSettingsWindow accountSettingsWindow = new AccountSettingsWindow();
            accountSettingsWindow.Show();
            Close();
        }

        private void AddTrainingButton_Click(object sender, RoutedEventArgs e)
        {
            TrainingWindow trainingWindow = new TrainingWindow();
            trainingWindow.Show();
            Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AppSettingsWindow appSettingsWindow = new AppSettingsWindow();
            appSettingsWindow.Show();
            Close();
        }

        private void AnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow analyticsWindow = new StatisticsWindow();
            analyticsWindow.Show();
            Close();

            // TODO
            return;
            MessageBox.Show("This feature is currently in development. Coming soon!", "Currently unavailable", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        private void OpenTrainingButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button pressedButton && pressedButton.DataContext is TrainingMainInfoDto selectedViewModelData)
            {
                string trainingTitleToOpen = selectedViewModelData.TrainingTitle;
                TrainingWindow trainingWindow = new TrainingWindow(trainingTitleToOpen);
                trainingWindow.Show();
                Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Escape))
            {
                MessageBoxResult exitDecisionResult = MessageBox.Show(TextRes.ExitConfirmationText, TextRes.ExitConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (exitDecisionResult.Equals(MessageBoxResult.Yes))
                {
                    Close();
                }
            }
        }
    }
}
