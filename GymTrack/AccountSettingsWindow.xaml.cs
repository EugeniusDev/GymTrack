using GymTrack.Resources.Languages;
using GymTrack.Settings;
using GymTrack.Themes;
using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GymTrack
{
    /// <summary>
    /// Interaction logic for AccountSettingsWindow.xaml
    /// </summary>
    public partial class AccountSettingsWindow : Window
    {
        public AswUIlanguageResources UILanguageRes { get; set; }
        public AswLanguageResources TextRes { get; set; }

        private EditButtonMode nameButtonMode = EditButtonMode.CanEdit,
            dayoffsButtonMode = EditButtonMode.CanEdit,
            weightButtonMode = EditButtonMode.CanEdit;

        private byte selectedDaysOff = 0;
        private readonly Button[] dayoffButtonsArray;

        private bool weightIsTracked = false;
        public AccountSettingsWindow()
        {
            TextRes = LanguageManager.PopulateASWlanguageResources();
            UILanguageRes = LanguageManager.PopulateASW_UIlanguageResources();
            DataContext = this;

            InitializeComponent();
            ThemeResourcesUpdater.UpdateTheme();
            dayoffButtonsArray = new[] {SundayDayoffButton, MondayDayoffButton, TuesdayDayoffButton,
                WednesdayDayoffButton, ThursdayDayoffButton, FridayDayoffButton,
                SaturdayDayoffButton };

            WeightTextBox.Text = UILanguageRes.WeightTextBoxDefault;
            if (Properties.Settings.Default.AccountConfigured)
            {
                AccountInfoTextBlock.Text = UILanguageRes.Header;
                NameTextBox.Text = Properties.Settings.Default.Username;
                selectedDaysOff = Properties.Settings.Default.Dayoffs;
                if (Properties.Settings.Default.IsWeightTracked)
                {
                    WeightTextBox.Text = Properties.Settings.Default.Weight.ToString();
                    TrackWeightCheckbox.IsChecked = true;
                }
                ExportAccountSettingsTextBlock.Visibility = Visibility.Visible;
                ExportAccountSettingsButton.Visibility = Visibility.Visible;
            }
            else
            {
                AccountInfoTextBlock.Text = UILanguageRes.HeaderNoAccount;
                NameTextBox.Text = UILanguageRes.NameTextBox;
            }
        }

        #region Name changing related
        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameButtonMode.Equals(EditButtonMode.CanEdit))
            {
                EnableNameEditingMode();
                return;
            }

            SaveNewName();
        }

        private void EnableNameEditingMode()
        {
            NameTextBox.Text = string.Empty;
            NameTextBox.Focusable = true;
            NameTextBox.Focus();
            EditNameButton.Content = UILanguageRes.EditButtonContent_SaveMode;
            nameButtonMode = EditButtonMode.CanSave;
        }

        private void SaveNewName()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show(TextRes.NameEntryErrorMessage, TextRes.WrongInputErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NameTextBox.Focusable = false;
            EditNameButton.Content = UILanguageRes.EditButtonContent_EditMode;
            nameButtonMode = EditButtonMode.CanEdit;
            Properties.Settings.Default.Username = NameTextBox.Text;
        }
        #endregion

        #region Dayoff changing related
        private void EditDaysoffButton_Click(object sender, RoutedEventArgs e)
        {
            if (dayoffsButtonMode.Equals(EditButtonMode.CanEdit))
            {
                ResetDayoffsAndMakeThemEditable();
                return;
            }

            SaveDayoffsAndMakeThemUneditable();
        }
        private void ResetDayoffsAndMakeThemEditable()
        {
            selectedDaysOff = 0;
            UpdateDayoffButtonsAppearance();
            for (int i = 0; i < dayoffButtonsArray.Length; i++)
            {
                dayoffButtonsArray[i].IsHitTestVisible = true;
            }

            EditDaysoffButton.Content = UILanguageRes.EditButtonContent_SaveMode;
            dayoffsButtonMode = EditButtonMode.CanSave;
        }
        private void SaveDayoffsAndMakeThemUneditable()
        {
            for (int i = 0; i < dayoffButtonsArray.Length; i++)
            {
                dayoffButtonsArray[i].IsHitTestVisible = false;
            }

            EditDaysoffButton.Content = UILanguageRes.EditButtonContent_EditMode;
            dayoffsButtonMode = EditButtonMode.CanEdit;
            Properties.Settings.Default.Dayoffs = selectedDaysOff;
        }
        private void DayoffButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button selectedDayoffButton))
            {
                return;
            }

            if (int.TryParse(selectedDayoffButton.Tag.ToString(), out int buttonIndex))
            {
                if ((selectedDaysOff & (1 << buttonIndex)) == 0)
                {
                    selectedDaysOff |= (byte)(1 << buttonIndex);
                }
                else
                {
                    selectedDaysOff &= (byte)~(1 << buttonIndex);
                }
            }

            UpdateDayoffButtonsAppearance();
        }

        private void DayoffButton_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDayoffButtonsAppearance();
        }

        private void UpdateDayoffButtonsAppearance()
        {
            for (int i = 0; i < dayoffButtonsArray.Length; i++)
            {
                if (!(dayoffButtonsArray[i].Template.FindName("EllipseBackground", dayoffButtonsArray[i]) is Ellipse ellipseOfCurrentButton))
                {
                    continue;
                }
                ellipseOfCurrentButton.Fill = (selectedDaysOff & (1 << i)) == 0 ?
                    (SolidColorBrush)FindResource("AccSW_DayoffButtonDefaultBackground")
                    : (SolidColorBrush)FindResource("AccSW_DayoffButtonSelectedBackground");
            }

            UpdateDayoffNote();
        }

        private void UpdateDayoffNote()
        {
            DayoffNoteTextBlock.Text = UILanguageRes.SelectedDayoffsInfo_Default;
            StringBuilder selectedDaysoffSB = new StringBuilder();
            ushort daysOffCount = 0;
            for (ushort i = 0; i < Enum.GetValues(typeof(DayOfWeek)).Length; i++)
            {
                // Check if day under index is selected
                if ((selectedDaysOff & (1 << i)) != 0)
                {
                    if (daysOffCount != 0)
                    {
                        selectedDaysoffSB.Append(", ");
                    }

                    switch ((DayOfWeek)i)
                    {
                        case DayOfWeek.Sunday:
                            selectedDaysoffSB.Append(TextRes.Sunday);
                            break;
                        case DayOfWeek.Monday:
                            selectedDaysoffSB.Append(TextRes.Monday);
                            break;
                        case DayOfWeek.Tuesday:
                            selectedDaysoffSB.Append(TextRes.Tuesday);
                            break;
                        case DayOfWeek.Wednesday:
                            selectedDaysoffSB.Append(TextRes.Wednesday);
                            break;
                        case DayOfWeek.Thursday:
                            selectedDaysoffSB.Append(TextRes.Thursday);
                            break;
                        case DayOfWeek.Friday:
                            selectedDaysoffSB.Append(TextRes.Friday);
                            break;
                        case DayOfWeek.Saturday:
                            selectedDaysoffSB.Append(TextRes.Saturday);
                            break;
                        default: break;
                    }
                    daysOffCount++;
                }
            }

            string resultOfSelection = selectedDaysoffSB.ToString();
            if (!string.IsNullOrWhiteSpace(resultOfSelection))
            {
                // If any day selected, display it
                DayoffNoteTextBlock.Text = $"{UILanguageRes.SelectedDayoffsInfo} {resultOfSelection}";
            }
        }
        #endregion

        #region Weight changing related
        private void EditWeightButton_Click(object sender, RoutedEventArgs e)
        {
            if (weightButtonMode.Equals(EditButtonMode.CanEdit))
            {
                EnableWeightEditingMode();
                return;
            }

            UpdateWeightInfo();
        }

        private void TrackWeightCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            weightIsTracked = true;
            EditWeightButton.Visibility = Visibility.Visible;
        }

        private void TrackWeightCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            weightIsTracked = false;
            EditWeightButton.Visibility = Visibility.Collapsed;
            UpdateWeightInfo();
        }

        private void EnableWeightEditingMode()
        {
            WeightTextBox.Text = string.Empty;
            WeightTextBox.Focusable = true;
            WeightTextBox.Focus();
            EditWeightButton.Content = UILanguageRes.EditButtonContent_SaveMode;
            weightButtonMode = EditButtonMode.CanSave;
        }

        private void UpdateWeightInfo()
        {
            EditWeightButton.Content = UILanguageRes.EditButtonContent_EditMode;
            weightButtonMode = EditButtonMode.CanEdit;

            WeightTextBox.Text = WeightTextBox.Text.Replace(".", ",");
            if (string.IsNullOrWhiteSpace(WeightTextBox.Text) || !float.TryParse(WeightTextBox.Text, out float newWeight) || newWeight < .1f)
            {
                MessageBox.Show(TextRes.WeightEntryErrorMessage, TextRes.WrongInputErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Properties.Settings.Default.IsWeightTracked = weightIsTracked;
            Properties.Settings.Default.Weight = newWeight;

            WeightTextBox.Focusable = false;
            EditWeightButton.Content = UILanguageRes.EditButtonContent_EditMode;
            weightButtonMode = EditButtonMode.CanEdit;
        }
        #endregion

        #region Import/Export account settings
        private void ImportAccountSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
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

            if (JsonConfigurationManager.ConfigureAccountSettingsFromJson(filePath))
            {
                NameTextBox.Text = Properties.Settings.Default.Username;
                selectedDaysOff = Properties.Settings.Default.Dayoffs;
                UpdateDayoffButtonsAppearance();
                weightIsTracked = Properties.Settings.Default.IsWeightTracked;
                if (weightIsTracked)
                {
                    WeightTextBox.Text = Properties.Settings.Default.Weight.ToString();
                }

                MessageBox.Show(TextRes.ImportSuccessMessage, TextRes.ImportSuccessTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            MessageBox.Show(TextRes.ImportErrorMessage, TextRes.ImportErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ExportAccountSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
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

            if (JsonConfigurationManager.WriteAccountSettingsToJson(filePath))
            {
                MessageBox.Show(TextRes.ExportSuccessMessage, TextRes.ExportSuccessTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBox.Show(TextRes.ExportErrorMessage, TextRes.ExportErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TryOpenMainMenu();
        }

        private void TryOpenMainMenu()
        {
            bool anythingIsInEditMode = nameButtonMode.Equals(EditButtonMode.CanSave)
                || dayoffsButtonMode.Equals(EditButtonMode.CanSave)
                || weightButtonMode.Equals(EditButtonMode.CanSave);
            if (anythingIsInEditMode)
            {
                MessageBox.Show(TextRes.SaveDataErrorMessage, TextRes.SaveDataErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool weightInfoIsEmpty = Properties.Settings.Default.Weight == 0 && Properties.Settings.Default.IsWeightTracked;
            bool nameInfoIsEmpty = string.IsNullOrWhiteSpace(Properties.Settings.Default.Username);
            if (weightInfoIsEmpty || nameInfoIsEmpty)
            {
                MessageBox.Show(TextRes.EmptyFieldsErrorMessage, TextRes.EmptyFieldsErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Properties.Settings.Default.AccountConfigured = true;
            Properties.Settings.Default.Save();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Escape))
            {
                TryOpenMainMenu();
            }
        }
    }
}
