using System;
using System.Windows;

namespace GymTrack.Themes
{
    /// <summary>
    /// Represents app theme updater that updates current theme resources of an app
    /// </summary>
    public static class ThemeResourcesUpdater
    {
        /// <summary>
        /// Updates theme of an app
        /// </summary>
        public static void UpdateTheme()
        {
            Theme currentTheme = (Theme)Properties.Settings.Default.ThemeID;
            switch (currentTheme)
            {
                case Theme.Light:
                    ChangeAppTheme(new Uri("Resources/Themes/LightTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Dark:
                    ChangeAppTheme(new Uri("Resources/Themes/DarkTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Barbie:
                    ChangeAppTheme(new Uri("Resources/Themes/BarbieTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Oppenheimer:
                    ChangeAppTheme(new Uri("Resources/Themes/OppenheimerTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Hacker:
                    ChangeAppTheme(new Uri("Resources/Themes/HackerTheme.xaml", UriKind.Relative));
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Changes used by app resource dictionary vie Uri
        /// </summary>
        /// <param name="themeUri">Uri of theme to switch to</param>
        private static void ChangeAppTheme(Uri themeUri)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary { Source = themeUri};

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
