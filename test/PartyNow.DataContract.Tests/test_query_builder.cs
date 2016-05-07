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
            var organizers = new int?[] {2, 3};
            var categories = new int?[] {5, 6};
            var places = new int?[] {7, 4};
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

        [Fact]
        public void should_return_valid_criteria_when_passing_null_values()
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var organizers = new int?[] { null };
            var categories = new int?[] { null };
            var places = new int?[] { null };
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
                                   $"startTicket={minPrice}&" +
                                   $"endTicket={maxPrice}&" +
                                   $"tickets={TicketType.tickets}");
        }
    }
}
