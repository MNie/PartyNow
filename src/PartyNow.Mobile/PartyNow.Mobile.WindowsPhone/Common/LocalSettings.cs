using Windows.Storage;

namespace PartyNow.Mobile.Common
{
    internal class LocalSettings
    {
        private static readonly ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;
        internal static void InitUrlsInStorageSettings()
        {
            Settings.Values["categoriesUrl"] = @"http://planer.info.pl/api/rest/categories.json";
            Settings.Values["organizersUrl"] = @"http://planer.info.pl/api/rest/organizers.json";
            Settings.Values["placesUrl"] = @"http://planer.info.pl/api/rest/places.json";
            Settings.Values["eventsUrl"] = @"http://planer.info.pl/api/rest/events.json";
        }

        internal static string GetUrl(string key)
        {
            if (Settings.Values.ContainsKey(key))
                return Settings.Values[key] as string;
            return "";
        }
    }
}
