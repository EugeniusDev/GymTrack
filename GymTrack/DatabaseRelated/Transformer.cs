using GymTrack.Objects;
using GymTrack.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GymTrack
{
    /// <summary>
    /// Used for minimal calculations related to training database and displaying info
    /// </summary>
    public static class Transformer
    {
        /// <summary>
        /// Dictionary used for trainsliteration (swapping character to optimal ones)
        /// </summary>
        private static Dictionary<char, string> transliterationTable = new Dictionary<char, string>
        {
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"},
            {'є', "ye"}, {'ж', "zh"}, {'з', "z"}, {'и', "y"}, {'і', "i"}, {'ї', "yi"},
            {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"},
            {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"}, {'ф', "f"},
            {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"}, {'ь', ""},
            {'ю', "yu"}, {'я', "ya"}, {'ґ', "g"}, {' ', "_"}, {'<', "_"}, {'>', "_"}, {':', "_"},
            {'\"', "_"}, {'/', "_"}, {'\\', "_"}, {'|', "_"}, {'?', "_"}, {'*', "_"}
        };

        /// <summary>
        /// Optimizes given string in terms of usage as file path: does transliteration
        /// </summary>
        /// <param name="inputString">String to trainsliterate</param>
        /// <returns>Transliterated string</returns>
        public static string GetTransliteratedString(string inputString)
        {
            foreach (var c in from char c in transliterationTable.Keys
                              where inputString.Contains(c)
                              select c)
            {
                inputString = inputString.Replace(c.ToString(), transliterationTable[c]);
            }

            return inputString;
        }

        /// <summary>
        /// Maps training objects into training general info objects
        /// </summary>
        /// <param name="trainingObjectsCollection">Collection of training objects to map</param>
        /// <returns>Collection of mapped training's main info objects</returns>
        public static List<TrainingMainInfoDto> MapTrainingObjectsToTrainingMainInfo(List<TrainingDto> trainingObjectsCollection)
        {
            if (trainingObjectsCollection.Count == 0)
            {
                return new List<TrainingMainInfoDto>();
            }

            List<TrainingMainInfoDto> trainingMainInfoDtos = new List<TrainingMainInfoDto>();
            StringBuilder trainingInfo = new StringBuilder();
            foreach (var trainingObject in trainingObjectsCollection)
            {
                if (trainingObject is null)
                {
                    continue;
                }

                float estimatedRestTime = GetEstimatedRestTime(trainingObject.ExerciseObjects.ToList());

                trainingInfo.Clear()
                    .AppendFormat("{0} {1} {2} ", trainingObject.ExerciseObjects.Count,
                        LanguageManager.GetStringByKey("Transformer_infoTextPart"),
                        estimatedRestTime);
                if (estimatedRestTime == 1)
                {
                    trainingInfo.Append(LanguageManager.GetStringByKey("Transformer_infoTextMinute"));
                }
                else
                {
                    trainingInfo.Append(LanguageManager.GetStringByKey("Transformer_infoTextMinutes"));
                }

                TrainingMainInfoDto currentTrainingMainInfoDto  = new TrainingMainInfoDto(){
                    TrainingTitle = trainingObject.TrainingTitle,
                    InfoAboutTraining = trainingInfo.ToString()
                };
                trainingMainInfoDtos.Add(currentTrainingMainInfoDto);
            }

            return trainingMainInfoDtos;
        }

        /// <summary>
        /// Calculates estimated rest time between sets of exercises based on their difficulty
        /// </summary>
        /// <param name="exerciseObjectsCollection">Collection of exercise objects</param>
        /// <returns>Float of estimated rest time</returns>
        private static float GetEstimatedRestTime(List<ExerciseDto> exerciseObjectsCollection)
        {
            float estimatedRestTime = 0;
            foreach (var exercise in exerciseObjectsCollection)
            {
                float multiplier = 0;
                switch (exercise.ExerciseDifficulty)
                {
                    case ExerciseDifficulty.Warmup:
                        multiplier = .5f;
                        break;
                    case ExerciseDifficulty.Easy:
                        multiplier = 2f;
                        break;
                    case ExerciseDifficulty.Medium:
                        multiplier = 3.5f;
                        break;
                    case ExerciseDifficulty.Hard:
                        multiplier = 5.5f;
                        break;
                    default:
                        multiplier = 6f;
                        break;
                }
                estimatedRestTime += exercise.SetsCount * multiplier;
            }

            return (float)Math.Round(estimatedRestTime, 1);
        }
    }
}
