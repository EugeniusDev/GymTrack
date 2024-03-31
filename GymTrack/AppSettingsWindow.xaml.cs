using GymTrack.DatabaseRelated;
using GymTrack.Resources.Languages;
using GymTrack.Settings;
using GymTrack.Themes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace GymTrack
{
    /// <summary>
    /// Interaction logic for AppSettingsWindow.xaml
    /// </summary>
    public partial class AppSettingsWindow : Window
    {
        public AppswUIlanguageResources UILanguageRes { get; set; }
        public AppswLanguageResources TextRes { get; set; }

        private bool windowIsLoaded = false;
        private ushort selectedThemeId = 0, selectedLanguageID = 0;
        private bool isTimeIn24Format;

        /// <summary>
        /// Window for configuring and changing application settings
        /// </summary>
        public AppSettingsWindow()
        {
            UILanguageRes = LanguageManager.PopulateAppSW_UIlanguageResources();
            TextRes = LanguageManager.PopulateAppSWlanguageResources();
            DataContext = this;

            InitializeComponent();
            selectedThemeId = Properties.Settings.Default.ThemeID;
            selectedLanguageID = Properties.Settings.Default.LanguageID;
            isTimeIn24Format = Properties.Settings.Default.IsTimeIn24HourFormat;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThemeCombobox.SelectedIndex = selectedThemeId;
            LanguageCombobox.SelectedIndex = selectedLanguageID;
            TimeFormatCombobox.SelectedIndex = isTimeIn24Format ? 0 : 1;
            windowIsLoaded = true;
            UpdateThemeDescription();
            UpdateLanguageDescription();
            UpdateTimeFormatDescription();
            UpdateTrainingFolderPathDescription();
        }

        #region Theme changing related
        private void ThemeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedThemeId = (ushort)ThemeCombobox.SelectedIndex;
            if (windowIsLoaded)
            {
                Properties.Settings.Default.ThemeID = selectedThemeId;
                Properties.Settings.Default.Save();
                ThemeResourcesUpdater.UpdateTheme();
                UpdateThemeDescription();
            }
        }

        private void UpdateThemeDescription()
        {
            string descriptionOfTheme = string.Empty;
            switch ((Theme)selectedThemeId)
            {
                case Theme.Light:
                    descriptionOfTheme = TextRes.LightThemeDescription;
                    break;
                case Theme.Dark:
                    descriptionOfTheme = TextRes.DarkThemeDescription;
                    break;
                case Theme.Barbie:
                    descriptionOfTheme = TextRes.BarbieThemeDescription;
                    break;
                case Theme.Oppenheimer: 
                    descriptionOfTheme = TextRes.OppenheimerThemeDescription;
                    break;
                case Theme.Hacker:
                    descriptionOfTheme = TextRes.HackerThemeDescription;
                    break;
                default: break;
            }
            ThemeInfoTextBlock.Text = descriptionOfTheme;
        }
        #endregion


        #region Language changing related
        private void LanguageCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLanguageID = (ushort)LanguageCombobox.SelectedIndex;
            if (windowIsLoaded)
            {
                Properties.Settings.Default.LanguageID = selectedLanguageID;
                Properties.Settings.Default.Save();
                UpdateLanguageOfApp();
                UpdateLanguageDescription();
            }
        }
        private void UpdateLanguageOfApp()
        {
            LanguageManager.UpdateCurrentLanguageResource();
            AppSettingsWindow reloadedAppSettingsWindow = new AppSettingsWindow();
            reloadedAppSettingsWindow.Show();
            Close();
        }
        private void UpdateLanguageDescription()
        {
            string descriptionOfLanguage = string.Empty;
            switch ((AppLanguage)selectedLanguageID)
            {
                case AppLanguage.English:
                    descriptionOfLanguage = TextRes.EnglishLanguageDescription;
                    break;
                case AppLanguage.LiteratureUkrainian:
                    descriptionOfLanguage = TextRes.LiteratureUkrainanLanguageDescription;
                    break;
                case AppLanguage.SpokenUkrainian:
                    descriptionOfLanguage = TextRes.SpokenUkrainanLanguageDescription;
                    break;
                default: break;
            }
            LanguageInfoTextBlock.Text = descriptionOfLanguage;
        }
        #endregion

        #region Time format changing related
        private void TimeFormatCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isTimeIn24Format = TimeFormatCombobox.SelectedIndex == 0;
            if (windowIsLoaded)
            {
                UpdateTimeFormatDescription();
                Properties.Settings.Default.IsTimeIn24HourFormat = isTimeIn24Format;
                Properties.Settings.Default.Save();
            }
        }

        private void UpdateTimeFormatDescription()
        {
            TimeFormatInfoTextBlock.Text = isTimeIn24Format ? TextRes.TimeFormat24hDescription
                : TextRes.TimeFormat12hDescription;
        }
        #endregion

        #region Changing folder where training data is stored
        private void ChangeTrainingDataPathButton_Click(object sender, RoutedEventArgs e)
        {
            string newFolderPath = string.Empty;
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    newFolderPath = dialog.SelectedPath;
                }
                else
                {
                    return;
                }
            }

            string deprecatedFolderPath = Properties.Settings.Default.TrainingFolderPath;
            Properties.Settings.Default.TrainingFolderPath = newFolderPath;
            Properties.Settings.Default.Save();

            TrainingWriter trainingWriter = new TrainingWriter();
            trainingWriter.MoveAllFilesToNewFolder(deprecatedFolderPath);

            UpdateTrainingFolderPathDescription();
        }

        private void UpdateTrainingFolderPathDescription()
        {
            string pathToDisplay = Properties.Settings.Default.TrainingFolderPath.Length > 60 ?
                $"{Properties.Settings.Default.TrainingFolderPath.Substring(0, 60)}..."
                : Properties.Settings.Default.TrainingFolderPath;
            CurrentTrainingDataFolderInfoTextBlock.Text = $"{TextRes.CurrentTrainingDataFolderText} {pathToDisplay}";
        }
        #endregion

        #region Import/Export app settings
        private void ImportAppSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = $"{TextRes.JsonFileTitle} (*.json)|*.json"
            };
            string filePath;
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            if (JsonConfigurationManager.ConfigureAppSettingsFromJson(filePath))
            {
                ThemeCombobox.SelectedIndex = Properties.Settings.Default.ThemeID;
                LanguageCombobox.SelectedIndex = Properties.Settings.Default.LanguageID;
                TimeFormatCombobox.SelectedIndex = Properties.Settings.Default.IsTimeIn24HourFormat ? 0 : 1;
                UpdateTrainingFolderPathDescription();

                System.Windows.MessageBox.Show(TextRes.ImportSuccessMessage, TextRes.ImportSuccessTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            System.Windows.MessageBox.Show(TextRes.ImportErrorMessage, TextRes.ImportErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ExportAppSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = $"{TextRes.JsonFileTitle} (*.json)|*.json"
            };
            string filePath;
            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            if (JsonConfigurationManager.WriteAppSettingsToJson(filePath))
            {
                System.Windows.MessageBox.Show(TextRes.ExportSuccessMessage, TextRes.ExportSuccessTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            System.Windows.MessageBox.Show(TextRes.ExportErrorMessage, TextRes.ExportErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion


        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Escape))
            {
                OpenMainMenu();
            }
        }
    }
}
