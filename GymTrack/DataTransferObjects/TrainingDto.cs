using GymTrack.Resources.Languages;
using System;
using System.Collections.ObjectModel;

namespace GymTrack.Objects
{
    /// <summary>
    /// Used for managing data of training (in TrainingWindow and TrainingCompletedWindow)
    /// </summary>
    [Serializable]
    public class TrainingDto
    {
        public string TrainingTitle {  get; set; }
        public ObservableCollection<ExerciseDto> ExerciseObjects {  get; set; }
        /// <summary>
        /// Parameterless constructor for creating new object with default values
        /// </summary>
        public TrainingDto()
        {
            LanguageManager.UpdateCurrentLanguageResource();
            TrainingTitle = LanguageManager.GetStringByKey("TrainingDto_trainingTitlePrompt");
            ExerciseObjects = new ObservableCollection<ExerciseDto>();
        }
        /// <summary>
        /// Constructor for creating training object with given data
        /// </summary>
        /// <param name="titleOfTraining">Title of training</param>
        /// <param name="exerciseObjects">Collection of exercise objects</param>
        public TrainingDto(string titleOfTraining, ObservableCollection<ExerciseDto> exerciseObjects)
        {
            TrainingTitle = titleOfTraining;
            ExerciseObjects = exerciseObjects;
        }
    }
}
