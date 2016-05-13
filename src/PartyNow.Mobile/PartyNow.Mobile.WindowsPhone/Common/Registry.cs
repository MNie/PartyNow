using Ninject;
using PartyNow.DataContract.Models;
using PartyNow.DataContract.Service;

namespace PartyNow.Mobile.Common
{
    public static class Registry
    {
        private static readonly IKernel Kernel = new StandardKernel();

        public static void Initialize()
        {
            Kernel.Bind<IBaseGetter<Categories>>().To<CategoriesGetter>().WithConstructorArgument<string>(LocalSettings.GetUrl(ConstValues.CategoriesUrl));
            Kernel.Bind<IBaseGetter<Places>>().To<PlacesGetter>().WithConstructorArgument<string>(LocalSettings.GetUrl(ConstValues.PlacesUrl));
            Kernel.Bind<IRichGetter<Events>>().To<EventsGetter>().WithConstructorArgument<string>(LocalSettings.GetUrl(ConstValues.EventsUrl));
            Kernel.Bind<IBaseGetter<Organizers>>().To<OrganizersGetter>().WithConstructorArgument<string>(LocalSettings.GetUrl(ConstValues.OrganizersUrl));
        }

        public static TItem Get<TItem>()
        {
            return Kernel.Get<TItem>();
        }
    }
}
