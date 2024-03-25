using System;
using System.Windows;

namespace GymTrack.Themes
{
    public static class ThemeResourcesUpdater
    {
        public static void UpdateTheme()
        {
            Theme currentTheme = (Theme)Properties.Settings.Default.ThemeID;
            switch (currentTheme)
            {
                case Theme.Light:
                    ThemeResourcesUpdater.ChangeAppTheme(new Uri("Resources/Themes/LightTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Dark:
                    ThemeResourcesUpdater.ChangeAppTheme(new Uri("Resources/Themes/DarkTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Barbie:
                    ThemeResourcesUpdater.ChangeAppTheme(new Uri("Resources/Themes/BarbieTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Oppenheimer:
                    ThemeResourcesUpdater.ChangeAppTheme(new Uri("Resources/Themes/OppenheimerTheme.xaml", UriKind.Relative));
                    break;
                case Theme.Hacker:
                    ThemeResourcesUpdater.ChangeAppTheme(new Uri("Resources/Themes/HackerTheme.xaml", UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        private static void ChangeAppTheme(Uri themeUri)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary { Source = themeUri};

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
