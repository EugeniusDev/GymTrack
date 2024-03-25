using GymTrack.Objects;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GymTrack.DatabaseRelated
{
    /// <summary>
    /// Used for reading training objects from binary file (deserializing them)
    /// </summary>
    public class TrainingReader : IDatabaseAccessible
    {
        private readonly string dataFolderName = Properties.Settings.Default.TrainingFolderPath;
        /// <summary>
        /// Gets all training objects from directory-database and adds them to list
        /// </summary>
        /// <returns>List<TrainingObject>, if they exist. If not, returns empty list</returns>
        public List<TrainingDto> GetAllTrainingObjects()
        {
            if (!DataFolderExists())
            {
                Directory.CreateDirectory(dataFolderName);
                return new List<TrainingDto>();
            }

            string[] allFilesFromDirectory = Directory.GetFiles(dataFolderName);
            if (allFilesFromDirectory.Length == 0)
            {
                return new List<TrainingDto>();
            }

            List<TrainingDto> trainingObjects = new List<TrainingDto>();
            foreach (string filePath in allFilesFromDirectory)
            {
                if (File.Exists(filePath))
                {
                    TrainingDto deserializedTrainingObject = DeserializeTrainingObject(filePath);
                    if (deserializedTrainingObject != null)
                    {
                        trainingObjects.Add(deserializedTrainingObject);
                    }
                }
            }

            return trainingObjects;
        }
        /// <summary>
        /// Gets all training paths from directory-database
        /// </summary>
        /// <returns>List<string>, if there are any trainings. If not, returns empty list</returns>
        public List<string> GetAllTrainingPaths(string deprecatedFolderPath)
        {
            if (!Directory.Exists(deprecatedFolderPath))
            {
                return new List<string>();
            }

            string[] allFilesFromDirectory = Directory.GetFiles(deprecatedFolderPath);
            if (allFilesFromDirectory.Length == 0)
            {
                return new List<string>();
            }

            List<string> pathStrings = new List<string>();
            foreach (string filePath in allFilesFromDirectory)
            {
                if (File.Exists(filePath) && DeserializeTrainingObject(filePath) != null)
                {
                    pathStrings.Add(filePath);
                }
            }

            return pathStrings;
        }
        /// <summary>
        /// Reads training object from file and deletes the file after read procedure
        /// </summary>
        /// <param name="titleOfTraining">Title of training to search for</param>
        /// <returns>Required training object; null if it does not exist</returns>
        public TrainingDto ReadAndDeleteTraining(string titleOfTraining)
        {
            string filePath = Path.Combine(dataFolderName, Transformer.GetTransliteratedString(titleOfTraining));
            if (DataFolderExists() && File.Exists(filePath))
            {
                TrainingDto deserializedTrainingObject = DeserializeTrainingObject(filePath);
                File.Delete(filePath);
                return deserializedTrainingObject;
            }

            return null;
        }
        /// <summary>
        /// Deserializes training object from binary file
        /// </summary>
        /// <param name="filePath">Path of binary file to read</param>
        /// <returns>Required training object; null if it does not exist</returns>
        private TrainingDto DeserializeTrainingObject(string filePath)
        {
            TrainingDto deserializedTrainingObject;
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    deserializedTrainingObject = formatter.Deserialize(stream) as TrainingDto;
                }
            }
            catch
            {
                return null;
            }

            return deserializedTrainingObject;
        }

        public bool DataFolderExists()
        {
            if (Directory.Exists(dataFolderName)) return true;
            return false;
        }
    }
}
