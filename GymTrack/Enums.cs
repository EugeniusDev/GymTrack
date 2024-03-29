namespace GymTrack
{
    /// <summary>
    /// Used for buttons Edit/Save
    /// </summary>
    public enum EditButtonMode
    {
        CanEdit,
        CanSave
    }
    /// <summary>
    /// Used for exercises manipulation
    /// </summary>
    public enum ExerciseDifficulty
    {
        Warmup,
        Easy,
        Medium,
        Hard
    }
    /// <summary>
    /// Used for choosing theme
    /// </summary>
    public enum Theme
    {
        Light,
        Dark,
        Barbie,
        Oppenheimer,
        Hacker
    }
    /// <summary>
    /// Used for choosing language
    /// </summary>
    public enum AppLanguage
    {
        English,
        LiteratureUkrainian,
        SpokenUkrainian
    }
    /// <summary>
    /// Used for choosing type of statistics to display
    /// </summary>
    public enum StatType
    {
        CompletionTime,
        Enforceability,
        Sets,
        Weight
    }
}
