using GymTrack.DatabaseRelated;
using GymTrack.Objects;
using GymTrack.Resources.Languages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GymTrack
{
    /// <summary>
    /// Interaction logic for TrainingWindow.xaml
    /// </summary>
    public partial class TrainingWindow : Window
    {
        public TwUIlanguageResources UILanguageRes { get; set; }
        public TWlanguageResources TextRes { get; set; }
        /// <summary>
        /// Mode of button that changes training title
        /// </summary>
        EditButtonMode trainingTitleEditButtonMode = EditButtonMode.CanEdit;
        /// <summary>
        /// Object of currently opened training
        /// </summary>
        TrainingDto currentTraining;

        #region Configuration of a window
        /// <summary>
        /// Parameterless constructor for mode of creating trainings
        /// </summary>
        public TrainingWindow()
        {
            currentTraining = new TrainingDto();
            InitializeComponent();
            NoExercisesYetTextBlock.Visibility = Visibility.Visible;
            TrainingTitleTextBox.Text = currentTraining.TrainingTitle;
        }
        /// <summary>
        /// Constructor that opens training by given title string
        /// </summary>
        /// <param name="titleOfTrainingToSearch">Title of training to open</param>
        public TrainingWindow(string titleOfTrainingToSearch)
        {
            TrainingReader trainingReader = new TrainingReader();
            currentTraining = trainingReader.ReadAndDeleteTraining(titleOfTrainingToSearch);
            InitializeComponent();
            // Checking if object is successfully deserialized
            bool readObjectIsNull = currentTraining is null;
            if (readObjectIsNull)
            {
                MessageBox.Show(TextRes.ReadingErrorMessage, TextRes.ReadingErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                OpenMainWindow();
            }

            TrainingTitleTextBox.Text = currentTraining.TrainingTitle;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigureLanguageResources();
            UpdateAllChangableGUI();
        }

        private void ConfigureLanguageResources()
        {
            UILanguageRes = LanguageManager.PopulateTW_UIlanguageResources();
            TextRes = LanguageManager.PopulateTWlanguageResources();
            DataContext = this;
            if (ExercisesListView.Items != null)
            {
                ExercisesListView.DataContext = UILanguageRes;
            }
        }
        #endregion

        #region Updating GUI related
        /// <summary>
        /// Updates appearance of ListView and TextBlocks that describe the training
        /// </summary>
        private void UpdateAllChangableGUI()
        {
            UpdateListView();
            UpdateNoExercisesText();
            UpdateDescriptionOfTraining();
        }
        /// <summary>
        /// Updates appearance of ListView in manner of re-assigning its ItemsSource. Used when collection of objects to display had changed
        /// </summary>
        private void UpdateListView()
        {
            ExercisesListView.ItemsSource = currentTraining.ExerciseObjects;
        }
        /// <summary>
        /// Updates visibility of TextBlock (visible when there are no exercises in the training)
        /// </summary>
        private void UpdateNoExercisesText()
        {
            if (currentTraining.ExerciseObjects.Count > 0)
            {
                NoExercisesYetTextBlock.Visibility = Visibility.Collapsed;
                return;
            }

            NoExercisesYetTextBlock.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Updates TextBlock with general info about training
        /// </summary>
        private void UpdateDescriptionOfTraining()
        {
            int exerciseCount = currentTraining.ExerciseObjects.Count;
            int totalSetsCount = 0;
            foreach(ExerciseDto exercise in currentTraining.ExerciseObjects)
            {
                totalSetsCount += exercise.SetsCount;
            }

            DescriptionOfTrainingTextBlock.Text = $"{TextRes.TotalExercisesText} {exerciseCount}{TextRes.TotalSetsText} {totalSetsCount}";
        }
        #endregion

        #region Manipulations with exercises
        /// <summary>
        /// Adds new sample exercise to corresponding collection and updates GUI for proper display
        /// </summary>
        private void AddNewExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            ExerciseDto newExercise = new ExerciseDto();
            currentTraining.ExerciseObjects.Add(newExercise);
            UpdateAllChangableGUI();
        }
        /// <summary>
        /// Gets the index of exercise that user clicked on
        /// </summary>
        /// <param name="eventSender">Button, used to get data of the exercise</param>
        /// <param name="indexToReturn">Index of the exercise in ListView</param>
        private void GetIndexOfSelectedExercise(object eventSender, out int indexToReturn)
        {
            indexToReturn = -1;
            if (eventSender is Button button && button.DataContext is ExerciseDto objectClicked)
            {
                indexToReturn = ExercisesListView.Items.IndexOf(objectClicked);
            }
        }

        private void DeleteExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            GetIndexOfSelectedExercise(sender, out int indexOfExercise);
            if (indexOfExercise >= 0)
            {
                MessageBoxResult confirmationPromptResult = MessageBox.Show($"{TextRes.ExerciseDeletionMessage} \"{currentTraining.ExerciseObjects[indexOfExercise].ExerciseTitle}\"?",
                    TextRes.ExerciseDeletionTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmationPromptResult == MessageBoxResult.Yes)
                {
                    currentTraining.ExerciseObjects.RemoveAt(indexOfExercise);
                    UpdateAllChangableGUI();
                }
            }
        }

        private void EditExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            GetIndexOfSelectedExercise(sender, out int indexOfObjectToEdit);
            // Find the parent ListViewItem of the clicked button
            ListViewItem itemContainer = GuiHelper.FindAncestor<ListViewItem>(button);

            if (button != null && indexOfObjectToEdit >= 0 && itemContainer.DataContext is ExerciseDto _)
            {
                TextBox exerciseTitleTextbox = GuiHelper.FindVisualChild<TextBox>(itemContainer, "ExerciseTitleTextBox");
                TextBox exerciseDescriptionTextbox = GuiHelper.FindVisualChild<TextBox>(itemContainer, "ExerciseDescriptionTextBox");
                TextBox exerciseSetsCountTextbox = GuiHelper.FindVisualChild<TextBox>(itemContainer, "ExerciseSetsCountTextBox");
                TextBox exerciseCustomMetricNameTextbox = GuiHelper.FindVisualChild<TextBox>(itemContainer, "ExerciseCustomMetricNameTextBox");
                TextBox exerciseCustomMetricValueTextbox = GuiHelper.FindVisualChild<TextBox>(itemContainer, "ExerciseCustomMetricValueTextBox");

                List<TextBox> editableTextBoxes = new List<TextBox>
                {
                    exerciseTitleTextbox,
                    exerciseDescriptionTextbox,
                    exerciseSetsCountTextbox,
                    exerciseCustomMetricNameTextbox,
                    exerciseCustomMetricValueTextbox
                };
                ComboBox exerciseDifficultyCombobox = GuiHelper.FindVisualChild<ComboBox>(itemContainer, "ExerciseDifficultyCombobox");

                if (currentTraining.ExerciseObjects[indexOfObjectToEdit].EditExerciseButtonMode == EditButtonMode.CanEdit)
                {
                    button.Content = UILanguageRes.EditButtonContent_SaveMode;
                    currentTraining.ExerciseObjects[indexOfObjectToEdit].EditExerciseButtonMode = EditButtonMode.CanSave;
                    // Make exercise focusable
                    exerciseDifficultyCombobox.IsHitTestVisible = true;
                    editableTextBoxes.ForEach(textbox => {
                        textbox.Focusable = true;
                        textbox.Background = (Brush)FindResource("TW_ActiveTextboxBackground");
                        textbox.BorderBrush = (Brush)FindResource("TW_ActiveElementBorderBrush");
                    });
                }
                else
                {
                    if (int.TryParse(exerciseSetsCountTextbox.Text.Trim(), out int setsCount) 
                        && int.TryParse(exerciseCustomMetricValueTextbox.Text.Trim(), out int customMetricValue))
                    {
                        // Saving input to data structure
                        currentTraining.ExerciseObjects[indexOfObjectToEdit].ExerciseTitle = exerciseTitleTextbox.Text.Trim();
                        currentTraining.ExerciseObjects[indexOfObjectToEdit].ExerciseDescription = exerciseDescriptionTextbox.Text.Trim();
                        currentTraining.ExerciseObjects[indexOfObjectToEdit].SetsCount = setsCount;
                        // If custom property info don't ends with ":", add it to the end
                        if (!exerciseCustomMetricNameTextbox.Text.EndsWith(":"))
                        {
                            exerciseCustomMetricNameTextbox.Text = string.Concat(exerciseCustomMetricNameTextbox.Text.Trim(), ":");
                        }

                        currentTraining.ExerciseObjects[indexOfObjectToEdit].CustomMetricName = exerciseCustomMetricNameTextbox.Text.Trim();
                        currentTraining.ExerciseObjects[indexOfObjectToEdit].CustomMetricValue = customMetricValue;
                        currentTraining.ExerciseObjects[indexOfObjectToEdit].ExerciseDifficulty = (ExerciseDifficulty)exerciseDifficultyCombobox.SelectedIndex;

                        button.Content = UILanguageRes.EditButtonContent_EditMode;
                        // Make exercise not focusable
                        exerciseDifficultyCombobox.IsHitTestVisible = false;
                        editableTextBoxes.ForEach(textbox => {
                            textbox.Focusable = false;
                            textbox.Background = (Brush)FindResource("TW_InactiveElementColor");
                            textbox.BorderBrush = (Brush)FindResource("TW_InactiveElementColor");
                        });

                        UpdateAllChangableGUI();
                        currentTraining.ExerciseObjects[indexOfObjectToEdit].EditExerciseButtonMode = EditButtonMode.CanEdit;
                    }
                    else
                    {
                        MessageBox.Show(TextRes.NotANumberErrorMessage, TextRes.WrongInputTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Manipulations with training
        // Not actually deletes the training
        private void DeleteTrainingAndOpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmationPromptResult = MessageBox.Show(TextRes.TrainingDeletionMessage,
                TextRes.TrainingDeletionTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmationPromptResult == MessageBoxResult.Yes)
            {
                OpenMainWindow();
            }
        }

        private void EditTrainingNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (trainingTitleEditButtonMode == EditButtonMode.CanEdit)
            {
                EditTrainingTitleButton.Content = UILanguageRes.EditButtonContent_SaveMode;
                TrainingTitleTextBox.Focusable = true;
                TrainingTitleTextBox.Focus();
                trainingTitleEditButtonMode = EditButtonMode.CanSave;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TrainingTitleTextBox.Text))
                {
                    MessageBox.Show(TextRes.EmptyTrainingTitleMessage, TextRes.WrongInputTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                EditTrainingTitleButton.Content = UILanguageRes.EditButtonContent_EditMode;
                currentTraining.TrainingTitle = TrainingTitleTextBox.Text;
                TrainingTitleTextBox.Focusable = false;
                trainingTitleEditButtonMode = EditButtonMode.CanEdit;
            }
        }

        // Determines if user is editing something
        private bool SomethingIsInEditMode()
        {
            if (trainingTitleEditButtonMode == EditButtonMode.CanSave)
            {
                return true;
            }

            foreach (var exercise in currentTraining.ExerciseObjects)
            {
                if (exercise.EditExerciseButtonMode == EditButtonMode.CanSave)
                {
                    return true;
                }
            }

            return false;
        }

        private bool TrainingContainsNoExercises()
        {
            if (currentTraining.ExerciseObjects.Count == 0)
            {
                return true;
            }

            return false;
        }

        private void CompleteTrainingButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTraining();
            TrainingCompletedWindow trainingCompletedWindow = new TrainingCompletedWindow(currentTraining);
            trainingCompletedWindow.Show();
            Close();
        }

        private void SaveTrainingAndOpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTraining();
            OpenMainWindow();
        }

        private void SaveTraining()
        {
            if (SomethingIsInEditMode())
            {
                MessageBox.Show(TextRes.SaveDataErrorMessage, TextRes.SaveDataErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TrainingContainsNoExercises())
            {
                MessageBox.Show(TextRes.EmptyTrainingErrorMessage, TextRes.EmptyTrainingErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TrainingWriter trainingWriter = new TrainingWriter();
            if (trainingWriter.TrainingAlreadyExists(currentTraining))
            {
                MessageBox.Show(TextRes.TrainingAlreadyExistsMessage, TextRes.TrainingAlreadyExistsTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (trainingWriter.WriteTrainingData(currentTraining))
            {
                return;
            }

            MessageBox.Show(TextRes.UnknownSavingErrorMessage, TextRes.UnknownSavingTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
