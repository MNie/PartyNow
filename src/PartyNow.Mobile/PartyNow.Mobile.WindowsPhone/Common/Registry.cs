using Windows.Devices.Geolocation;
using InsertIt.Common;
using PartyNow.DataContract.Models;
using PartyNow.DataContract.Service;

namespace PartyNow.Mobile.Common
{
    public static class Registry
    {
        private static Container _container;

        public static void Initialize()
        {
            _container = new Container(_ =>
            {
                _.Record<IBaseGetter<Categories>>().As<CategoriesGetter>().Ctor<string>(LocalSettings.GetUrl(ConstValues.CategoriesUrl));
                _.Record<IBaseGetter<Places>>().As<PlacesGetter>().Ctor<string>(LocalSettings.GetUrl(ConstValues.PlacesUrl));
                _.Record<IRichGetter<Events>>().As<EventsGetter>().Ctor<string>(LocalSettings.GetUrl(ConstValues.EventsUrl));
                _.Record<IBaseGetter<Organizers>>().As<OrganizersGetter>().Ctor<string>(LocalSettings.GetUrl(ConstValues.OrganizersUrl));
                _.Record<Geolocator>().As<Geolocator>();
            });
        }

        public static TItem Get<TItem>()
        {
            return _container.Resolve<TItem>();
        }
    }
}
