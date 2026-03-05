using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void ChangeLanguage(string languageCode)
        {
            ResourceDictionary dict = new ResourceDictionary();

            string uriPath = $"Resources/Lang/Lang.{languageCode}.xaml";

            try
            {
                dict.Source = new Uri(uriPath, UriKind.Relative);
            }
            catch
            {
                return;
            }

            ResourceDictionary oldDict = null;

            foreach (ResourceDictionary d in Current.Resources.MergedDictionaries)
            {
                if (d.Contains("LoginTitle"))
                {
                    oldDict = d;
                    break;
                }
            }

            if (oldDict != null)
            {
                Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            Current.Resources.MergedDictionaries.Add(dict);
        }

        public static void ChangeTheme(string themeName)
        {
            ResourceDictionary dict = new ResourceDictionary();
            string uriPath = $"Resources/Themes/Theme.{themeName}.xaml";

            try
            {
                dict.Source = new Uri(uriPath, UriKind.Relative);
            }
            catch { return; }

            ResourceDictionary oldDict = null;

            foreach (ResourceDictionary d in Current.Resources.MergedDictionaries)
            {
                if (d.Contains("IsThemeDictionary"))
                {
                    oldDict = d;
                    break;
                }
            }

            if (oldDict != null)
            {
                Current.Resources.MergedDictionaries.Remove(oldDict);
            }
            Current.Resources.MergedDictionaries.Add(dict);
        }
    }

}
