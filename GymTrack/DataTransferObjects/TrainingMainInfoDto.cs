using GymTrack.Resources.Languages;

namespace GymTrack
{
    /// <summary>
    /// Used for displaying info of training in MainWindow
    /// </summary>
    public class TrainingMainInfoDto
    {
        public string TrainingTitle { get; set; }
        public string InfoAboutTraining { get; set; }
        public string OpenTrainingButtonContent { get; set; }
        public TrainingMainInfoDto()
        {
            OpenTrainingButtonContent = LanguageManager.GetStringByKey("MW_openTrainingButtonContent"); 
        }
    }
}
