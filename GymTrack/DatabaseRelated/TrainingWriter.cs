using GymTrack.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Documents;

namespace GymTrack.DatabaseRelated
{
    /// <summary>
    /// Used for saving training object into binary file (serializing it)
    /// </summary>
    public class TrainingWriter : IDatabaseAccessible
    {
        private readonly string dataFolderName = Properties.Settings.Default.TrainingFolderPath;
        /// <summary>
        /// Checks if file with same name as title of passed object exists to determine possible data loss
        /// </summary>
        /// <param name="trainingToCheck">Training object that is required to perform check operation</param>
        /// <returns>true, if file already exists; false if not</returns>
        public bool TrainingAlreadyExists(TrainingDto trainingToCheck)
        {
            if (!DataFolderExists())
            {
                Directory.CreateDirectory(dataFolderName);
                return false;
            }

            string fileNameFromTrainingTitle = Transformer.GetTransliteratedString(trainingToCheck.TrainingTitle);
            string pathOfFileToCheck = Path.Combine(dataFolderName, fileNameFromTrainingTitle);
            if (File.Exists(pathOfFileToCheck))
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Serializes object into file with name of transliterated given training object's title
        /// </summary>
        /// <param name="trainingToWrite">Object of training that needs to be serialized</param>
        /// <returns>true, if write completed successfully; false if not</returns>
        public bool WriteTrainingData(TrainingDto trainingToWrite)
        {
            if (!DataFolderExists() || trainingToWrite is null)
            {
                return false;
            }

            string fileNameFromTrainingTitle = Transformer.GetTransliteratedString(trainingToWrite.TrainingTitle);
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(dataFolderName, fileNameFromTrainingTitle), FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, trainingToWrite);
                }
            } catch { return false; }

            return true;
        }
        /// <summary>
        /// Moves all files with training objects into a newly defined folder-database
        /// </summary>
        /// <param name="deprecatedFolderPath">Path of an old folder-database</param>
        /// <returns>true, if move operations completed successfully; false if not</returns>
        public bool MoveAllFilesToNewFolder(string deprecatedFolderPath)
        {
            TrainingReader trainingReader = new TrainingReader();
            List<string> filesToMove = trainingReader.GetAllTrainingPaths(deprecatedFolderPath);
            if (filesToMove.Count == 0)
            {
                return true;
            }
            try
            {
                foreach (string originalFilePath in filesToMove)
                {
                    if (File.Exists(originalFilePath))
                    {
                        string clearFileName = Path.GetFileName(originalFilePath);
                        string newFilePath = Path.Combine(dataFolderName, clearFileName);
                        File.Move(originalFilePath, newFilePath);
                    }
                }
            } catch { return false; }

            return true;
        }
        public bool DataFolderExists()
        {
            if (Directory.Exists(dataFolderName)) return true;
            return false;
        }
    }
}
