using GymTrack.Objects;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GymTrack.DataTransferObjects;
using GymTrack.Resources.Languages;
using GymTrack.Themes;
using GymTrack.DatabaseRelated;

namespace GymTrack
{
    /// <summary>
    /// Interaction logic for TrainingCompletedWindow.xaml
    /// </summary>
    public partial class TrainingCompletedWindow : Window
    {
        TrainingDto currentTraining;
        ObservableCollection<ExerciseMainInfoDto> exerciseMainInfos = new ObservableCollection<ExerciseMainInfoDto>();
        public TcwUIlanguageResources UILanguageRes { get; set; }
        public TcWlanguageResources TextRes { get; set; }

        /// <summary>
        /// Window for marking the training completed
        /// </summary>
        /// <param name="trainingToBeDone">Object of training to be marked completed</param>
        public TrainingCompletedWindow(TrainingDto trainingToBeDone)
        {
            LanguageManager.UpdateCurrentLanguageResource();
            ThemeResourcesUpdater.UpdateTheme();
            TextRes = LanguageManager.PopulateTCompWlanguageResources();
            UILanguageRes = LanguageManager.PopulateTCompW_UIlanguageResources();
            DataContext = this;

            currentTraining = trainingToBeDone;
            foreach (var exercise in currentTraining.ExerciseObjects)
            {
                exerciseMainInfos.Add(new ExerciseMainInfoDto(exercise.ExerciseTitle, exercise.SetsCount));
            }

            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExercisesListView.ItemsSource = exerciseMainInfos;
            TrainingTitleTextBlock.Text = currentTraining.TrainingTitle;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TrainingWindow trainingWindow = new TrainingWindow(currentTraining.TrainingTitle);
            trainingWindow.Show();
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            int sumOfSetsCount = 0;
            foreach (var exercise in currentTraining.ExerciseObjects)
            {
                sumOfSetsCount += exercise.SetsCount;
            }

            StatisticsStampDto statisticsStampDto = new StatisticsStampDto(currentTraining.TrainingTitle, sumOfSetsCount);
            if (!StatisticsWriter.AppendObjectToStatisticsFile(statisticsStampDto))
            {
                MessageBox.Show(TextRes.SavingErrorMessage, TextRes.SavingErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        #region Modification of sets count
        private void GetIndexOfSelectedExercise(object eventSender, out int indexToReturn)
        {
            indexToReturn = -1;
            if (eventSender is Button button && button.DataContext is ExerciseMainInfoDto objectClicked)
            {
                indexToReturn = ExercisesListView.Items.IndexOf(objectClicked);
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            GetIndexOfSelectedExercise(sender, out int indexOfItemToEdit);

            // Find the parent ListViewItem of the clicked button
            ListViewItem itemContainer = GuiHelper.FindAncestor<ListViewItem>(button);

            if (button != null && indexOfItemToEdit >= 0 && itemContainer.DataContext is ExerciseMainInfoDto _)
            {
                TextBlock exerciseSetsCountTextblock = GuiHelper.FindVisualChild<TextBlock>(itemContainer, "ExerciseSetsCountTextBlock");
                UpdateSetsCount(indexOfItemToEdit, exerciseSetsCountTextblock, false);
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            GetIndexOfSelectedExercise(sender, out int indexOfItemToEdit);

            // Find the parent ListViewItem of the clicked button
            ListViewItem itemContainer = GuiHelper.FindAncestor<ListViewItem>(button);

            if (button != null && indexOfItemToEdit >= 0 && itemContainer.DataContext is ExerciseMainInfoDto _)
            {
                TextBlock exerciseSetsCountTextblock = GuiHelper.FindVisualChild<TextBlock>(itemContainer, "ExerciseSetsCountTextBlock");
                UpdateSetsCount(indexOfItemToEdit, exerciseSetsCountTextblock, true);
            }
        }

        /// <summary>
        /// Updates done sets count in both TrainingDto and ListView
        /// </summary>
        /// <param name="indexOfItemToEdit">Index of item in list of items</param>
        /// <param name="textBlockToUpdate">TextBlock of current item</param>
        /// <param name="shouldBeIncremented">true, if increment needed; false, if decrement needed</param>
        private void UpdateSetsCount(int indexOfItemToEdit, TextBlock textBlockToUpdate, bool shouldBeIncremented)
        {
            int currentSetsCount = currentTraining.ExerciseObjects[indexOfItemToEdit].SetsCount;
            if (currentSetsCount <= 0 && !shouldBeIncremented)
            {
                return;
            }

            currentSetsCount = shouldBeIncremented ? ++currentSetsCount : --currentSetsCount;

            exerciseMainInfos[indexOfItemToEdit].UpdateSetsCountText(currentSetsCount);
            textBlockToUpdate.Text = exerciseMainInfos[indexOfItemToEdit].ExerciseSetsCountText;

            currentTraining.ExerciseObjects[indexOfItemToEdit].SetsCount = currentSetsCount;
        }
        #endregion
    }
}
