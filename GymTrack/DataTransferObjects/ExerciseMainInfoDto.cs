using GymTrack.Resources.Languages;

namespace GymTrack.DataTransferObjects
{
    public class ExerciseMainInfoDto
    {
        public string ExerciseTitle { get; set; }
        public string ExerciseSetsCountText { get; set; }
        private readonly string setsTextBoilerplate;
        public ExerciseMainInfoDto(string exerciseTitle, int setsCount)
        {
            ExerciseTitle = exerciseTitle;
            setsTextBoilerplate = LanguageManager.GetStringByKey("exerciseMainInfoSetsText");
            ExerciseSetsCountText = $"{setsTextBoilerplate} {setsCount}";
        }
        public void UpdateSetsCountText(int newSetsCount)
        {
            ExerciseSetsCountText = $"{setsTextBoilerplate} {newSetsCount}";
        }
    }
}
