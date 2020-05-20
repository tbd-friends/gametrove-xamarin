using System;
using Gametrove.Core.Resources.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gametrove.Core.Infrastructure
{
    public static class ThemeHelper
    {
        public static void SetCurrentTheme(this ResourceDictionary resources)
        {
            resources.MergedDictionaries.Clear();

            var themeName = Preferences.Get(AppPreferences.ApplicationTheme, null);

            var currentTheme = themeName != null ? (Theme)Enum.Parse(typeof(Theme), themeName) : Theme.Default;

            switch (currentTheme)
            {
                case Theme.Junicus:
                    resources.MergedDictionaries.Add(new Junicus());
                    break;
                case Theme.SuperNintendo:
                    resources.MergedDictionaries.Add(new SuperNintendo());
                    break;
                default:
                    resources.MergedDictionaries.Add(new Default());
                    break;
            }

        }
    }
}