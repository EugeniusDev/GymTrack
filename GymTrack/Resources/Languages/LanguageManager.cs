using System.Reflection;
using System.Resources;

namespace GymTrack.Resources.Languages
{
    /// <summary>
    /// Represents a language manager that populates required strings or updates current source of them
    /// </summary>
    public static class LanguageManager
    {
        private static string resourceToUse = "GymTrack.Resources.Languages.English";
        /// <summary>
        /// Updates resource language table to a current language
        /// </summary>
        public static void UpdateCurrentLanguageResource()
        {
            ushort currentLanguageID = Properties.Settings.Default.LanguageID;
            switch ((AppLanguage)currentLanguageID)
            {
                case AppLanguage.English:
                    resourceToUse = "GymTrack.Resources.Languages.English";
                    break;
                case AppLanguage.LiteratureUkrainian:
                    resourceToUse = "GymTrack.Resources.Languages.LiteratureUkrainian";
                    break;
                case AppLanguage.SpokenUkrainian:
                    resourceToUse = "GymTrack.Resources.Languages.SpokenUkrainian";
                    break;
                default:
                    resourceToUse = "GymTrack.Resources.Languages.English";
                    break;
            }
        }
        /// <summary>
        /// Gets string value of language table row by string key
        /// </summary>
        /// <param name="key">String key of required value</param>
        /// <returns>Value of language table row</returns>
        public static string GetStringByKey(string key)
        {
            ResourceManager resourceManager = new ResourceManager(resourceToUse, Assembly.GetExecutingAssembly());
            return resourceManager.GetString(key);
        }

        #region MainWindow resource structures population
        public static MwUIlanguageResources PopulateMW_UIlanguageResources()
        {
            return new MwUIlanguageResources
            {
                NoTrainingsText = GetStringByKey("MW_noTrainingsText"),
                WelcomeText = $"{GetStringByKey("MW_welcomeText")}{Properties.Settings.Default.Username}",
                AccountButtonContent = GetStringByKey("MW_accountButtonContent"),
                TrainingButtonContent = GetStringByKey("MW_trainingButtonContent"),
                AnalyticsButtonContent = GetStringByKey("MW_analyticsButtonContent"),
                SettingsButtonContent = GetStringByKey("MW_settingsButtonContent"),
                WindowTitle = GetStringByKey("MW_windowTitle")
            };
        }
        public static MwlanguageResources PopulateMWlanguageResources()
        {
            return new MwlanguageResources
            {
                ExitConfirmationText = GetStringByKey("MW_exitConfirmationText"),
                ExitConfirmationTitle = GetStringByKey("MW_exitConfirmationTitle")
            };
        }

        #endregion

        #region AccountSettingsWindow resource structures population
        public static AswUIlanguageResources PopulateASW_UIlanguageResources()
        {
            return new AswUIlanguageResources
            {
                WindowTitle = GetStringByKey("ASW_windowTitle"),
                HeaderNoAccount = GetStringByKey("ASW_headerNoAccount"),
                Header = GetStringByKey("ASW_header"),
                SubHeader = GetStringByKey("settingsSubHeader"),
                NameTextBlock = GetStringByKey("ASW_nameTextBlock"),
                NameTextBox = GetStringByKey("ASW_nameTextBox"),
                EditButtonContent_EditMode = GetStringByKey("editButtonEditModeContent"),
                EditButtonContent_SaveMode = GetStringByKey("editButtonSaveModeContent"),
                EditButtonContent = GetStringByKey("editButtonEditModeContent"),
                DayoffsTextBlock = GetStringByKey("ASW_dayoffsTextBlock"),
                ShortForMonday = GetStringByKey("ASW_dayoffsMonday"),
                ShortForTuesday = GetStringByKey("ASW_dayoffsTuesday"),
                ShortForWednesday = GetStringByKey("ASW_dayoffsWednesday"),
                ShortForThursday = GetStringByKey("ASW_dayoffsThursday"),
                ShortForFriday = GetStringByKey("ASW_dayoffsFriday"),
                ShortForSaturday = GetStringByKey("ASW_dayoffsSaturday"),
                ShortForSunday = GetStringByKey("ASW_dayoffsSunday"),
                SelectedDayoffsInfo_Default = GetStringByKey("ASW_dayoffsInfoTextBlockDefault"),
                WeightTextBlock = GetStringByKey("ASW_weightTextBlock"),
                WeightTextBoxDefault = GetStringByKey("ASW_weightTextBoxDefault"),
                TrackWeightTextBlock = GetStringByKey("ASW_weightTrackTextBlock"),
                ImportTextBlock = GetStringByKey("ASW_importAccountTextBlock"),
                ExportTextBlock = GetStringByKey("ASW_exportAccountTextBlock"),
                ImportButton = GetStringByKey("ASW_importAccountButtonContent"),
                ExportButton = GetStringByKey("ASW_exportAccountButtonContent"),
                MainMenuButton = GetStringByKey("mainMenuButtonContent"),
                SelectedDayoffsInfo = GetStringByKey("ASW_dayoffsInfoTextBlock")
            };
        }
        public static AswLanguageResources PopulateASWlanguageResources()
        {
            return new AswLanguageResources
            {
                Monday = GetStringByKey("ASW_monday"),
                Tuesday = GetStringByKey("ASW_tuesday"),
                Wednesday = GetStringByKey("ASW_wednesday"),
                Thursday = GetStringByKey("ASW_thursday"),
                Friday = GetStringByKey("ASW_friday"),
                Saturday = GetStringByKey("ASW_saturday"),
                Sunday = GetStringByKey("ASW_sunday"),
                NameEntryErrorMessage = GetStringByKey("ASW_nameEntryError"),
                WrongInputErrorTitle = GetStringByKey("wrongInputTitle"),
                WeightEntryErrorMessage = GetStringByKey("ASW_weightEntryError"),
                JsonFileTitle = GetStringByKey("jsonFileTitle"),
                ImportSuccessMessage = GetStringByKey("successfullSettingsImportMessage"),
                ImportSuccessTitle = GetStringByKey("successfullSettingsImportTitle"),
                ImportErrorMessage = GetStringByKey("settingsImportErrorMessage"),
                ImportErrorTitle = GetStringByKey("settingsImportErrorTitle"),
                ExportSuccessMessage = GetStringByKey("successfullSettingsExportMessage"),
                ExportSuccessTitle = GetStringByKey("successfullSettingsExportTitle"),
                ExportErrorMessage = GetStringByKey("settingsExportErrorMessage"),
                ExportErrorTitle = GetStringByKey("settingsExportErrorTitle"),
                SaveDataErrorMessage = GetStringByKey("saveDataErrorMessage"),
                SaveDataErrorTitle = GetStringByKey("saveDataErrorTitle"),
                EmptyFieldsErrorMessage = GetStringByKey("fillFieldsErrorMessage"),
                EmptyFieldsErrorTitle = GetStringByKey("fillFieldsErrorTitle")
            };
        }
        #endregion

        #region AppSettingsWindow resource structures population
        public static AppswUIlanguageResources PopulateAppSW_UIlanguageResources()
        {
            return new AppswUIlanguageResources
            {
                WindowTitle = GetStringByKey("AppSW_windowTitle"),
                Header = GetStringByKey("AppSW_header"),
                SubHeader = GetStringByKey("settingsSubHeader"),
                ThemeTextBlock = GetStringByKey("AppSW_themeTextBlock"),
                LanguageTextBlock = GetStringByKey("AppSW_languageTextBlock"),
                TimeFormatTextBlock = GetStringByKey("AppSW_timeFormatTextBlock"),
                TrainingFolderPathTextBlock = GetStringByKey("AppSW_trainingFolderPathTextBlock"),
                LightThemeTitle = GetStringByKey("AppSW_lightThemeComboboxItem"),
                DarkThemeTitle = GetStringByKey("AppSW_darkThemeComboboxItem"),
                BarbieThemeTitle = GetStringByKey("AppSW_barbieThemeComboboxItem"),
                OppenheimerThemeTitle = GetStringByKey("AppSW_oppenheimerThemeComboboxItem"),
                HackerThemeTitle = GetStringByKey("AppSW_hackerThemeComboboxItem"),
                EnglishLanguageTitle = GetStringByKey("AppSW_engLanguageComboboxItem"),
                LiteratureUkrainanLanguageTitle = GetStringByKey("AppSW_literatureUkrLanguageComboboxItem"),
                SpokenUkrainanLanguageTitle = GetStringByKey("AppSW_spokenUkrLanguageComboboxItem"),
                TimeFormat24hTitle = GetStringByKey("AppSW_24formatComboboxItem"),
                TimeFormat12hTitle = GetStringByKey("AppSW_12formatComboboxItem"),
                FolderButton = GetStringByKey("AppSW_folderButtonContent"),
                ImportTextBlock = GetStringByKey("AppSW_importSettingsTextBlock"),
                ExportTextBlock = GetStringByKey("AppSW_exportSettingsTextBlock"),
                ImportButton = GetStringByKey("AppSW_importAppSettingsButtonContent"),
                ExportButton = GetStringByKey("AppSW_exportAppSettingsButtonContent"),
                MainMenuButton = GetStringByKey("mainMenuButtonContent")
            };
        }
        public static AppswLanguageResources PopulateAppSWlanguageResources()
        {
            return new AppswLanguageResources
            {
                LightThemeDescription = GetStringByKey("AppSW_lightThemeDescription"),
                DarkThemeDescription = GetStringByKey("AppSW_darkThemeDescription"),
                BarbieThemeDescription = GetStringByKey("AppSW_barbieThemeDescription"),
                OppenheimerThemeDescription = GetStringByKey("AppSW_oppenheimerThemeDescription"),
                HackerThemeDescription = GetStringByKey("AppSW_hackerThemeDescription"),
                EnglishLanguageDescription = GetStringByKey("AppSW_engLanguageDescription"),
                LiteratureUkrainanLanguageDescription = GetStringByKey("AppSW_literatureUkrLanguageDescription"),
                SpokenUkrainanLanguageDescription = GetStringByKey("AppSW_spokenUkrLanguageDescription"),
                TimeFormat24hDescription = GetStringByKey("AppSW_24formatDescription"),
                TimeFormat12hDescription = GetStringByKey("AppSW_12formatDescription"),
                CurrentTrainingDataFolderText = GetStringByKey("AppSW_currentTrainingsFolderText"),
                JsonFileTitle = GetStringByKey("jsonFileTitle"),
                ImportSuccessMessage = GetStringByKey("successfullSettingsImportMessage"),
                ImportSuccessTitle = GetStringByKey("successfullSettingsImportTitle"),
                ImportErrorMessage = GetStringByKey("settingsImportErrorMessage"),
                ImportErrorTitle = GetStringByKey("settingsImportErrorTitle"),
                ExportSuccessMessage = GetStringByKey("successfullSettingsExportMessage"),
                ExportSuccessTitle = GetStringByKey("successfullSettingsExportTitle"),
                ExportErrorMessage = GetStringByKey("settingsExportErrorMessage"),
                ExportErrorTitle = GetStringByKey("settingsExportErrorTitle")
            };
        }
        #endregion

        #region TrainingWindow resource structures population
        public static TwUIlanguageResources PopulateTW_UIlanguageResources()
        {
            return new TwUIlanguageResources
            {
                WindowTitle = GetStringByKey("TW_windowTitle"),
                NoExercisesTextBlock = GetStringByKey("TW_noExercisesTextBlock"),
                DeleteButtonContent = GetStringByKey("TW_deleteButtonContent"),
                EditButtonContent = GetStringByKey("editButtonEditModeContent"),
                NewExerciseButtonContent = GetStringByKey("TW_newExerciseButtonContent"),
                SaveAndBackToMainMenuButton = GetStringByKey("TW_saveAndBackToMainButtonContent"),
                MarkCompletedButtonContent = GetStringByKey("TW_markAsCompletedButtonContent"),
                EditButtonContent_EditMode = GetStringByKey("editButtonEditModeContent"),
                EditButtonContent_SaveMode = GetStringByKey("editButtonSaveModeContent")
            };
        }
        public static TWlanguageResources PopulateTWlanguageResources()
        {
            return new TWlanguageResources
            {
                TotalExercisesText = GetStringByKey("TW_totalExercisesText"),
                TotalSetsText = GetStringByKey("TW_totalSetsText"),
                TrainingDeletionMessage = GetStringByKey("TW_trainingDeletionPromptMessage"),
                TrainingDeletionTitle = GetStringByKey("TW_trainingDeletionPromptTitle"),
                EmptyTrainingTitleMessage = GetStringByKey("TW_trainingTitleErrorMessage"),
                WrongInputTitle = GetStringByKey("wrongInputTitle"),
                EmptyTrainingErrorMessage = GetStringByKey("TW_emptyTrainingListErrorMessage"),
                EmptyTrainingErrorTitle = GetStringByKey("TW_emptyTrainingListErrorTitle"),
                TrainingAlreadyExistsMessage = GetStringByKey("TW_trainingAlreadyExistsErrorMessage"),
                TrainingAlreadyExistsTitle = GetStringByKey("TW_trainingAlreadyExistsErrorTitle"),
                UnknownSavingErrorMessage = GetStringByKey("TW_unknownSavingErrorMessage"),
                UnknownSavingTitle = GetStringByKey("TW_unknownSavingErrorTitle"),
                SaveDataErrorMessage = GetStringByKey("saveDataErrorMessage"),
                SaveDataErrorTitle = GetStringByKey("saveDataErrorTitle"),
                NotANumberErrorMessage = GetStringByKey("TW_enterNumberErrorMessage"),
                ReadingErrorMessage = GetStringByKey("TW_emptyTrainingObjectErrorMessage"),
                ReadingErrorTitle = GetStringByKey("TW_emptyTrainingObjectErrorTitle"),
                ExerciseDeletionMessage = GetStringByKey("TW_exerciseDeletionPromptMessage"),
                ExerciseDeletionTitle = GetStringByKey("TW_exerciseDeletionPromptTitle")
            };
        }
        #endregion

        #region TrainingCompletedWindow resource structures population
        public static TcwUIlanguageResources PopulateTCompW_UIlanguageResources()
        {
            return new TcwUIlanguageResources
            {
                WindowTitle = GetStringByKey("TCompW_windowTitle"),
                CancelButtonContent = GetStringByKey("TCompW_cancelButtonContent"),
                ConfirmButtonContent = GetStringByKey("TCompW_confirmButtonContent")
            };
        }
        public static TcWlanguageResources PopulateTCompWlanguageResources()
        {
            return new TcWlanguageResources
            {
                SavingErrorTitle = GetStringByKey("TW_unknownSavingErrorTitle"),
                SavingErrorMessage = GetStringByKey("TW_unknownSavingErrorMessage")
            };
        }
        #endregion

        #region StatisticsWindow resource structures population
        public static StatisticswUIlanguageResources PopulateStatysticsw_UIlanguageResources()
        {
            return new StatisticswUIlanguageResources
            {
                WindowTitle = GetStringByKey("StatW_windowTitle"),
                HeaderFirstPart = GetStringByKey("StatW_headerPart1"),
                NoDataText = GetStringByKey("StatW_noDataText"),
                CompletionTimeStatButton = GetStringByKey("StatW_compTimeStatButtonContent"),
                EnforceabilityStatButton = GetStringByKey("StatW_enforceabilityStatButtonContent"),
                SetsStatButton = GetStringByKey("StatW_setsStatButtonContent"),
                WeightStatButton = GetStringByKey("StatW_weightStatButtonContent"),
                MainMenuButton = GetStringByKey("mainMenuButtonContent")
            };
        }
        public static StatisticswLanguageResources PopulateStatysticswlanguageResources()
        {
            return new StatisticswLanguageResources
            {
                CompletionTimeStatChapter = GetStringByKey("StatW_compTimeStatHeaderPart"),
                EnforceabilityStatChapter = GetStringByKey("StatW_enforceabilityTimeStatHeaderPart"),
                SetsStatChapter = GetStringByKey("StatW_setsStatHeaderPart"),
                WeightStatChapter = GetStringByKey("StatW_weightTimeStatHeaderPart")
            };
        }
        #endregion

        #region ExerciseDto UI resource structure population
        public static ExerciseDtoUIstuff PopulateExerciseUI()
        {
            return new ExerciseDtoUIstuff
            {
                DifficultyComboboxTitle = GetStringByKey("TW_difficultyComboboxTitle"),
                WarmupComboboxItem = GetStringByKey("TW_difficultyWarmupComboboxItem"),
                EasyComboboxItem = GetStringByKey("TW_difficultyEasyComboboxItem"),
                MediumComboboxItem = GetStringByKey("TW_difficultyMediumComboboxItem"),
                HardComboboxItem = GetStringByKey("TW_difficultyHardComboboxItem"),
                SetsCountTextBlock = GetStringByKey("TW_setsCountTextBlock"),
                EditButtonContent = GetStringByKey("editButtonEditModeContent"),
                DeleteButtonContent = GetStringByKey("TW_deleteButtonContent")
            };
        }
        #endregion
    }
}
