using GymTrack.DataTransferObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GymTrack.DatabaseRelated
{
    public static class StatisticsWriter
    {
        /// <summary>
        /// Appends given StatisticsStampDto object data into a statistics file
        /// </summary>
        /// <param name="statisticsObject">Object to append</param>
        /// <returns>true, if operation completed successfully; false, if not</returns>
        public static bool AppendObjectToStatisticsFile(StatisticsStampDto statisticsObject)
        {
            string directoryPath = Properties.Settings.Default.StatisticsFolderName;
            // Ensure that directory exists
            Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, Properties.Settings.Default.StatisticsFileName);
            try
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    string statObjectInJsonString = JsonConvert.SerializeObject(statisticsObject);
                    sw.WriteLine(statObjectInJsonString);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Deletes current statistics file and rewrites it with reduced list
        /// </summary>
        /// <param name="statObjList">Reduced list of statistics objects</param>
        public static void OverwriteStatisticsFile(List<StatisticsStampDto> statObjList)
        {
            string directoryPath = Properties.Settings.Default.StatisticsFolderName;
            string filePath = Path.Combine(directoryPath, Properties.Settings.Default.StatisticsFileName);
            File.Delete(filePath);

            statObjList.ForEach(obj => AppendObjectToStatisticsFile(obj));
        }
    }
}
