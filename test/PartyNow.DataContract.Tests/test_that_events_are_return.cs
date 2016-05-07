using System.Linq;
using PartyNow.DataContract.Service;
using Shouldly;
using Xunit;

namespace PartyNow.DataContract.Tests
{
    public class data_contract_tests
    {
        [Fact]
        public void should_return_valid_events_list()
        {
            var serviceCaller = new EventsGetter(@"http://planer.info.pl/api/rest/events.json");

            var events = serviceCaller.Get().Result.ToList();

            events.Count().ShouldBeGreaterThan(0);
            events.ForEach(@event =>
            {
                @event.categoryId.ShouldBeGreaterThan(0);
                @event.name.ShouldNotBeNullOrWhiteSpace();
            });
        }

        [Fact]
        public void should_return_valid_organizers_list()
        {
            var serviceCaller = new OrganizersGetter(@"http://planer.info.pl/api/rest/organizers.json");

            var organizers = serviceCaller.Get().Result.ToList();

            organizers.Count().ShouldBeGreaterThan(0);
            organizers.ForEach(organizer =>
            {
                organizer.id.Value.ShouldBeGreaterThan(0);
                organizer.designation.ShouldNotBeNullOrWhiteSpace();
            });
        }

        [Fact]
        public void should_return_valid_categories_list()
        {
            var serviceCaller = new CategoriesGetter(@"http://planer.info.pl/api/rest/categories.json");

            var categories = serviceCaller.Get().Result.ToList();
            categories.Count().ShouldBeGreaterThan(0);
            categories.ForEach(category =>
            {
                category.name.ShouldNotBeNullOrWhiteSpace();
            });
        }

        [Fact]
        public void should_return_valid_places_list()
        {
            var serviceCaller = new PlacesGetter(@"http://planer.info.pl/api/rest/places.json");

            var places = serviceCaller.Get().Result.ToList();
            places.Count().ShouldBeGreaterThan(0);
            places.ForEach(place =>
            {
                place.address.ShouldNotBeNull();
            });
        }
    }
}
