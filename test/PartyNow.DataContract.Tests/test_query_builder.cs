using System;
using PartyNow.DataContract.Common;
using Shouldly;
using Xunit;

namespace PartyNow.DataContract.Tests
{
    public class test_query_builder
    {
        [Fact]
        public void should_return_valid_criteria()
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var organizers = new[] {2, 3};
            var categories = new[] {5, 6};
            var places = new[] {7, 4};
            var minPrice = 10;
            var maxPrice = 20;
            var query = new QueryBuilder()
                .WhereStartDateIs(date)
                .WhereEndDateIs(date)
                .WhereOrganizerIs(organizers)
                .WhereCategoryIs(categories)
                .WherePlaceIs(places)
                .WhereStartTicketPriceIs(minPrice)
                .WhereEndTicketPriceIs(maxPrice)
                .WhereTypeOfTicketsIs(TicketType.tickets);

            var finalQuery = query.CreateQuery();

            finalQuery.ShouldBe($"?start_date={date}&" +
                                   $"end_date={date}&" +
                                   $"organizer=[{string.Join(",", organizers)}]&" +
                                   $"category=[{string.Join(",", categories)}]&" +
                                   $"place=[{string.Join(",", places)}]&" +
                                   $"startTicket={minPrice}&" +
                                   $"endTicket={maxPrice}&" +
                                   $"tickets={TicketType.tickets}");
        }
    }
}
