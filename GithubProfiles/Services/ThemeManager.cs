using System.Collections.Generic;
using GithubProfiles.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GithubProfiles.Services
{
    public class ThemeManager
    {

        public static void SetTheme(bool darkMode)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                Preferences.Set("dark_theme", darkMode);

                if (darkMode)
                    mergedDictionaries.Add(new DarkTheme());
                else
                    mergedDictionaries.Add(new LightTheme());
            }
        }

        public static void LoadTheme()
        {
            SetTheme(Preferences.Get("dark_theme", false));
        }
    }
}
