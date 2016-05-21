using System.Collections.Generic;

namespace PartyNow.Mobile.Common
{
    class ConstValues
    {
        public const string CategoriesUrl = "categoriesUrl";
        public const string OrganizersUrl = "organizersUrl";
        public const string PlacesUrl = "placesUrl";
        public const string EventsUrl = "eventsUrl";

        public class Communicates
        {
            public const string ExitApp = "Czy na chcesz zamknąć aplikację?";
            public const string Yes = "Tak";
            public const string No = "No";
        }

        public const string AllOptions = "Wszystkie";
        public static IEnumerable<string> EventPropertiesToExclude = new List<string>()
        {
            "id",
            "urls",
            "attachments",
            "descLong",
            "categoryId",
            "descShort"
        };
    }
}
