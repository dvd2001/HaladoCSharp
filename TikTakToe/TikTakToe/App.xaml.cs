using System;
using System.Windows;

namespace TikTakToe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ThemeToggle(bool isDarkMode)
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri(isDarkMode ? "Themes/DarkTheme.xaml" : "Themes/LightTheme.xaml", UriKind.Relative)
            };
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(dict);
        }

    }
}
