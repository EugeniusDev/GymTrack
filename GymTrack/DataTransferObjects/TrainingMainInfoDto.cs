using GymTrack.Resources.Languages;
using System.ComponentModel;

namespace GymTrack
{
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
