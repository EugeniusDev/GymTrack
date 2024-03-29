using GymTrack.DataTransferObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GymTrack.DatabaseRelated
{
    public static class StatisticsReader
    {
        /// <summary>
        /// Reads list of StatisticsStampDto objects from statistics file
        /// </summary>
        /// <returns>List of StatisticsStampDto objects or empty list, if there are no objects</returns>
        public static List<StatisticsStampDto> ReadStatObjectsFromDatabase()
        {
            List<StatisticsStampDto> statObjList = new List<StatisticsStampDto>();
            string directoryPath = Properties.Settings.Default.StatisticsFolderName;
            string filePath = Path.Combine(directoryPath, Properties.Settings.Default.StatisticsFileName);
            // Ensure that file exist
            if (!File.Exists(filePath))
            {
                return statObjList;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        StatisticsStampDto obj = JsonConvert.DeserializeObject<StatisticsStampDto>(line);
                        statObjList.Add(obj);
                    }
                    catch
                    {
                        // Skip the unreadable data
                    }
                }
            }

            // Make list smaller if it is too big
            if (statObjList.Count > 100)
            {
                statObjList.RemoveRange(0, 20);
                StatisticsWriter.OverwriteStatisticsFile(statObjList);
            }

            return statObjList;
        }
    }
}
