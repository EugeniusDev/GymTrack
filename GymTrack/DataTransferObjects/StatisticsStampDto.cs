using System;

namespace GymTrack.DataTransferObjects
{
    /// <summary>
    /// Used for storing data into statistics database-file and later usage of it
    /// </summary>
    [Serializable]
    public class StatisticsStampDto
    {
        public string TrainingTitle { get; set; }
        public DateTime DateOfTrainingCompletion { get; set; }
        public int SetsCountSum { get; set; }
        public float Weight { get; set; }
        public StatisticsStampDto(string trainingTitle, int sumOfSetsCount)
        {
            TrainingTitle = trainingTitle;
            DateOfTrainingCompletion = DateTime.Now;
            SetsCountSum = sumOfSetsCount;
            Weight = Properties.Settings.Default.Weight;
        }
    }
}
