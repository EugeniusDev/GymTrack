using System;

namespace GymTrack.Resources.Languages
{
    #region MainWindow related
    /// <summary>
    /// Structure for holding MainWindow UI related resources
    /// </summary>
    public struct MwUIlanguageResources
    {
        public string NoTrainingsText { get; set; }
        public string WelcomeText { get; set; }
        public string AccountButtonContent { get; set; }
        public string TrainingButtonContent { get; set; }
        public string AnalyticsButtonContent { get; set; }
        public string SettingsButtonContent { get; set; }
        public string WindowTitle { get; set; }
    }
    /// <summary>
    /// Structure for holding MainWindow interaction logic related resources
    /// </summary>
    public struct MwlanguageResources
    {
        public string ExitConfirmationText { get; set; }
        public string ExitConfirmationTitle { get; set; }
    }
    #endregion

    #region AccountSettingsWindow related
    /// <summary>
    /// Structure for holding AccountSettingsWindow UI related resources
    /// </summary>
    public struct AswUIlanguageResources
    {
        public string WindowTitle { get; set; }
        public string HeaderNoAccount { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string NameTextBlock { get; set; }
        public string NameTextBox { get; set; }
        public string EditButtonContent { get; set; }
        public string EditButtonContent_EditMode { get; set; }
        public string EditButtonContent_SaveMode { get; set; }
        public string DayoffsTextBlock { get; set; }
        public string ShortForMonday { get; set; }
        public string ShortForTuesday { get; set; }
        public string ShortForWednesday { get; set; }
        public string ShortForThursday { get; set; }
        public string ShortForFriday { get; set; }
        public string ShortForSaturday { get; set; }
        public string ShortForSunday { get; set; }
        public string SelectedDayoffsInfo_Default { get; set; }
        public string WeightTextBlock { get; set; }
        public string WeightTextBoxDefault { get; set; }
        public string TrackWeightTextBlock { get; set; }
        public string ImportTextBlock { get; set; }
        public string ExportTextBlock { get; set; }
        public string ImportButton { get; set; }
        public string ExportButton { get; set; }
        public string MainMenuButton { get; set; }
        public string SelectedDayoffsInfo { get; set; }
    }
    /// <summary>
    /// Structure for holding AccountSettingsWindow interaction logic related resources
    /// </summary>
    public struct AswLanguageResources
    {
        public string Monday {  get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
        public string NameEntryErrorMessage { get; set; }
        public string WrongInputErrorTitle { get; set; }
        public string WeightEntryErrorMessage { get; set; }
        public string JsonFileTitle { get; set; }
        public string ImportSuccessMessage { get; set; }
        public string ImportSuccessTitle { get; set; }
        public string ImportErrorMessage { get; set; }
        public string ImportErrorTitle { get; set; }
        public string ExportSuccessMessage { get; set; }
        public string ExportSuccessTitle { get; set; }
        public string ExportErrorMessage { get; set; }
        public string ExportErrorTitle { get; set; }
        public string SaveDataErrorMessage { get; set; }
        public string SaveDataErrorTitle { get; set; }
        public string EmptyFieldsErrorMessage { get; set; }
        public string EmptyFieldsErrorTitle { get; set; }
    }
    #endregion

    #region AppSettingsWindow related
    /// <summary>
    /// Structure for holding AppSettingsWindow UI related resources
    /// </summary>
    public struct AppswUIlanguageResources
    {
        public string WindowTitle { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string ThemeTextBlock { get; set; }
        public string LanguageTextBlock { get; set; }
        public string TimeFormatTextBlock { get; set; }
        public string TrainingFolderPathTextBlock { get; set; }
        public string LightThemeTitle { get; set; }
        public string DarkThemeTitle { get; set; }
        public string BarbieThemeTitle { get; set; }
        public string OppenheimerThemeTitle { get; set; }
        public string HackerThemeTitle { get; set; }
        public string EnglishLanguageTitle {  get; set; }
        public string LiteratureUkrainanLanguageTitle { get; set; }
        public string SpokenUkrainanLanguageTitle { get; set; }
        public string TimeFormat24hTitle { get; set; }
        public string TimeFormat12hTitle { get; set; }
        public string FolderButton { get; set; }
        public string ImportTextBlock { get; set; }
        public string ExportTextBlock { get; set; }
        public string ImportButton { get; set; }
        public string ExportButton { get; set; }
        public string MainMenuButton { get; set; }
    }
    /// <summary>
    /// Structure for holding AppSettingsWindow interaction logic related resources
    /// </summary>
    public struct AppswLanguageResources
    {
        public string LightThemeDescription { get; set; }
        public string DarkThemeDescription { get; set; }
        public string BarbieThemeDescription { get; set; }
        public string OppenheimerThemeDescription { get; set; }
        public string HackerThemeDescription { get; set; }
        public string EnglishLanguageDescription { get; set; }
        public string LiteratureUkrainanLanguageDescription { get; set; }
        public string SpokenUkrainanLanguageDescription { get; set; }
        public string TimeFormat24hDescription { get; set; }
        public string TimeFormat12hDescription{ get; set; }
        public string CurrentTrainingDataFolderText { get; set; }
        public string JsonFileTitle { get; set; }
        public string ImportSuccessMessage { get; set; }
        public string ImportSuccessTitle { get; set; }
        public string ImportErrorMessage { get; set; }
        public string ImportErrorTitle { get; set; }
        public string ExportSuccessMessage { get; set; }
        public string ExportSuccessTitle { get; set; }
        public string ExportErrorMessage { get; set; }
        public string ExportErrorTitle { get; set; }
    }
    #endregion

    #region TrainingWindow related
    /// <summary>
    /// Structure for holding TrainingWindow UI related resources
    /// </summary>
    public struct TwUIlanguageResources
    {
        public string WindowTitle { get; set; }
        public string NoExercisesTextBlock { get; set; }
        public string DeleteButtonContent { get; set; }
        public string EditButtonContent { get; set; }
        public string NewExerciseButtonContent { get; set; }
        public string SaveAndBackToMainMenuButton { get; set; }
        public string MarkCompletedButtonContent { get; set; }
        public string EditButtonContent_EditMode { get; set; }
        public string EditButtonContent_SaveMode { get; set; }
    }
    /// <summary>
    /// Structure for holding TrainingWindow interaction logic related resources
    /// </summary>
    public struct TWlanguageResources
    {
        public string TotalExercisesText { get; set; }
        public string TotalSetsText { get; set; }
        public string TrainingDeletionMessage {  get; set; }
        public string TrainingDeletionTitle { get; set; }
        public string EmptyTrainingTitleMessage { get; set; }
        public string WrongInputTitle {  get; set; }
        public string EmptyTrainingErrorMessage { get; set; }
        public string EmptyTrainingErrorTitle { get; set; }
        public string TrainingAlreadyExistsMessage { get; set; }
        public string TrainingAlreadyExistsTitle { get; set; }
        public string UnknownSavingErrorMessage { get; set; }
        public string UnknownSavingTitle { get; set; }
        public string SaveDataErrorMessage {  get; set; }
        public string SaveDataErrorTitle {  get; set; }
        public string NotANumberErrorMessage { get; set; }
        public string ReadingErrorMessage { get; set; }
        public string ReadingErrorTitle { get; set; }
        public string ExerciseDeletionMessage { get; set; }
        public string ExerciseDeletionTitle { get; set; }
    }
    #endregion

    #region TrainingCompletedWindow related
    /// <summary>
    /// Structure for holding TrainingCompletedWindow UI related resources
    /// </summary>
    public struct TcwUIlanguageResources
    {
        public string WindowTitle { get; set; }
        public string CancelButtonContent { get; set; }
        public string ConfirmButtonContent { get; set; }
    }
    /// <summary>
    /// Structure for holding TrainingCompletedWindow interaction logic related resources
    /// </summary>
    public struct TcWlanguageResources
    {
        public string SavingErrorTitle { get; set; }
        public string SavingErrorMessage { get; set; }
    }
    #endregion

    #region StatisticsWindow related
    /// <summary>
    /// Structure for holding StatisticsWindow UI related resources
    /// </summary>
    public struct StatisticswUIlanguageResources
    {
        public string WindowTitle { get; set; }
        public string HeaderFirstPart { get; set; }
        public string NoDataText { get; set; }
        public string CompletionTimeStatButton { get; set; }
        public string EnforceabilityStatButton { get; set; }
        public string SetsStatButton { get; set; }
        public string WeightStatButton { get; set; }
        public string MainMenuButton { get; set; }
    }
    /// <summary>
    /// Structure for holding StatisticsWindow interaction logic related resources
    /// </summary>
    public struct StatisticswLanguageResources
    {
        public string CompletionTimeStatChapter { get; set; }
        public string EnforceabilityStatChapter { get; set; }
        public string SetsStatChapter { get; set; }
        public string WeightStatChapter { get; set; }
    }
    #endregion

    /// <summary>
    /// Structure for holding UI-related resources of exercise
    /// </summary>
    [Serializable]
    public struct ExerciseDtoUIstuff
    {
        public string DifficultyComboboxTitle { get; set; }
        public string WarmupComboboxItem { get; set; }
        public string EasyComboboxItem { get; set; }
        public string MediumComboboxItem { get; set; }
        public string HardComboboxItem { get; set; }
        public string SetsCountTextBlock { get; set; }
        public string EditButtonContent { get; set; }
        public string DeleteButtonContent { get; set; }
    }
}
