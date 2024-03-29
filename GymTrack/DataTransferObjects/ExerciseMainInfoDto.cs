using GymTrack.Resources.Languages;

namespace GymTrack.DataTransferObjects
{
    /// <summary>
    /// Used for data of exercises in window of marking training as completed (in TrainingCompletedWindow)
    /// </summary>
    public class ExerciseMainInfoDto
    {
        public string ExerciseTitle { get; set; }
        public string ExerciseSetsCountText { get; set; }
        private readonly string setsTextBoilerplate;
        /// <summary>
        /// Populates exercise object with title and text about sets count
        /// </summary>
        /// <param name="exerciseTitle">Title of an exercise</param>
        /// <param name="setsCount">Count of sets of an exercise</param>
        public ExerciseMainInfoDto(string exerciseTitle, int setsCount)
        {
            ExerciseTitle = exerciseTitle;
            setsTextBoilerplate = LanguageManager.GetStringByKey("exerciseMainInfoSetsText");
            ExerciseSetsCountText = $"{setsTextBoilerplate} {setsCount}";
        }
        /// <summary>
        /// Update text about sets count in case of changing sets count
        /// </summary>
        /// <param name="newSetsCount">Updated count of sets</param>
        public void UpdateSetsCountText(int newSetsCount)
        {
            ExerciseSetsCountText = $"{setsTextBoilerplate} {newSetsCount}";
        }
    }
}
