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
    }
}
