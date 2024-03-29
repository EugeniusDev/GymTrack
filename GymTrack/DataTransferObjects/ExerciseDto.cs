using GymTrack.Resources.Languages;
using System;

namespace GymTrack
{
    /// <summary>
    /// Used for transfering and managing data of exercise while training is opened (in TrainingWindow)
    /// </summary>
    [Serializable]
    public class ExerciseDto
    {
        public string ExerciseTitle { get; set; }
        public string ExerciseDescription { get; set; }
        public ExerciseDifficulty ExerciseDifficulty { get; set; }
        public int SetsCount { get; set; }
        public string CustomMetricName { get; set; }
        public int CustomMetricValue { get; set; }
        public EditButtonMode EditExerciseButtonMode { get; set; }
        public ExerciseDtoUIstuff ExerciseUI { get; set; }

        /// <summary>
        /// Populates object with sample data
        /// </summary>
        public ExerciseDto()
        {
            LanguageManager.UpdateCurrentLanguageResource();
            ExerciseTitle = LanguageManager.GetStringByKey("ExerciseDto_titlePrompt");
            ExerciseDescription = LanguageManager.GetStringByKey("ExerciseDto_descriptionPrompt");
            ExerciseDifficulty = ExerciseDifficulty.Warmup;
            SetsCount = 2;
            CustomMetricName = LanguageManager.GetStringByKey("ExerciseDto_customPropertyPrompt");
            CustomMetricValue = 5;
            ExerciseUI = LanguageManager.PopulateExerciseUI();
        }
    }
}
