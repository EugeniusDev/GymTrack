using Newtonsoft.Json;
using System.IO;

namespace GymTrack.Settings
{
    /// <summary>
    /// Configuration data mapper and writer
    /// </summary>
    public static class JsonConfigurationManager
    {
        /// <summary>
        /// Structure of account settings data
        /// </summary>
        public struct AccountSettingsData
        {
            public string Username { get; set; }
            public byte Dayoffs { get; set; }
            public bool IsWeightTracked { get; set; }
            public float Weight { get; set; }
        }
        /// <summary>
        /// Structure of application settings data
        /// </summary>
        public struct AppSettingsData
        {
            public string TrainingFolderName { get; set; }
            public ushort ThemeID { get; set; }
            public ushort LanguageID { get; set; }
            public bool IsTimeIn24HourFormat { get; set; }
        }

        /// <summary>
        /// Writes account settings object (formed on current properties data) into JSON file
        /// </summary>
        /// <param name="filePath">Path to export settings into</param>
        /// <returns>true, if written successfully; false if not</returns>
        public static bool WriteAccountSettingsToJson(string filePath)
        {
            AccountSettingsData accountSettingsObject = new AccountSettingsData
            {
                Username = Properties.Settings.Default.Username,
                Dayoffs = Properties.Settings.Default.Dayoffs,
                IsWeightTracked = Properties.Settings.Default.IsWeightTracked,
                Weight = Properties.Settings.Default.Weight,
            };
            try
            {
                string accountSettingsInJsonString = JsonConvert.SerializeObject(accountSettingsObject);
                File.WriteAllText(filePath, accountSettingsInJsonString);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads account settings from corresponding file
        /// </summary>
        /// <param name="filePath">Chosen directory to export settings into</param>
        /// <returns>true, if read successfully; false if not</returns>
        public static bool ConfigureAccountSettingsFromJson(string filePath)
        {
            try
            {
                string accountSettingsInJsonString = File.ReadAllText(filePath);
                AccountSettingsData readFromJsonAccountSettings = JsonConvert.DeserializeObject<AccountSettingsData>(accountSettingsInJsonString);

                Properties.Settings.Default.Username = readFromJsonAccountSettings.Username;
                Properties.Settings.Default.Dayoffs = readFromJsonAccountSettings.Dayoffs;
                Properties.Settings.Default.IsWeightTracked = readFromJsonAccountSettings.IsWeightTracked;
                Properties.Settings.Default.Weight = readFromJsonAccountSettings.Weight;
                Properties.Settings.Default.AccountConfigured = true;
                Properties.Settings.Default.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Writes application settings object (formed on current properties data) into JSON file
        /// </summary>
        /// <param name="filePath">Path to export settings into</param>
        /// <returns>true, if written successfully; false if not</returns>
        public static bool WriteAppSettingsToJson(string filePath)
        {
            AppSettingsData appSettingsObject = new AppSettingsData
            {
                TrainingFolderName = Properties.Settings.Default.TrainingFolderPath,
                ThemeID = Properties.Settings.Default.ThemeID,
                LanguageID = Properties.Settings.Default.LanguageID,
                IsTimeIn24HourFormat = Properties.Settings.Default.IsTimeIn24HourFormat
            };
            try
            {
                string appSettingsInJsonString = JsonConvert.SerializeObject(appSettingsObject);
                File.WriteAllText(filePath, appSettingsInJsonString);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads application settings from corresponding file
        /// </summary>
        /// <param name="filePath">Chosen directory to export settings into</param>
        /// <returns>true, if read successfully; false if not</returns>
        public static bool ConfigureAppSettingsFromJson(string filePath)
        {
            try
            {
                string appSettingsInJsonString = File.ReadAllText(filePath);
                AppSettingsData readFromJsonAppSettings = JsonConvert.DeserializeObject<AppSettingsData>(appSettingsInJsonString);

                Properties.Settings.Default.TrainingFolderPath = readFromJsonAppSettings.TrainingFolderName;
                Properties.Settings.Default.ThemeID = readFromJsonAppSettings.ThemeID;
                Properties.Settings.Default.LanguageID = readFromJsonAppSettings.LanguageID;
                Properties.Settings.Default.IsTimeIn24HourFormat = readFromJsonAppSettings.IsTimeIn24HourFormat;
                Properties.Settings.Default.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
