using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PartyNow.DataContract.Common
{
    public class QueryBuilder
    {
        private readonly Dictionary<string, string> QueryParameters;

        public QueryBuilder()
        {
            QueryParameters = new Dictionary<string, string>()
            {
                {EventsParametersConstants.StartDate, null },
                {EventsParametersConstants.EndDate, null },
                {EventsParametersConstants.Organizer, null },
                {EventsParametersConstants.Category, null },
                {EventsParametersConstants.Place, null },
                {EventsParametersConstants.StartTicket, null },
                {EventsParametersConstants.EndTicket, null },
                {EventsParametersConstants.Tickets, null },
            };
        }
        public QueryBuilder WhereStartDateIs(string startDate)
        {
            return WhereSingleOptionIs(EventsParametersConstants.StartDate, startDate);
        }

        public QueryBuilder WhereEndDateIs(string endDate)
        {
            return WhereSingleOptionIs(EventsParametersConstants.EndDate, endDate);
        }

        public QueryBuilder WhereOrganizerIs(int?[] ids)
        {
            return WhereMultiOptionalIs(EventsParametersConstants.Organizer, ids);
        }

        public QueryBuilder WhereCategoryIs(int?[] ids)
        {
            return WhereMultiOptionalIs(EventsParametersConstants.Category, ids);
        }

        public QueryBuilder WherePlaceIs(int?[] ids)
        {
            return WhereMultiOptionalIs(EventsParametersConstants.Place, ids);
        }

        private QueryBuilder WhereMultiOptionalIs(string key, int?[] ids)
        {
            var idToSerialize = ids.Where(x => x != null);
            if(idToSerialize.Any())
                QueryParameters[key] = JsonConvert.SerializeObject(idToSerialize);
            return this;
        }

        private QueryBuilder WhereSingleOptionIs(string key, string value)
        {
            if(value != null)
                QueryParameters[key] = value;
            return this;
        }

        public QueryBuilder WhereStartTicketPriceIs(int? minPrice)
        {
            return WhereSingleOptionIs(EventsParametersConstants.StartTicket, minPrice.ToString());
        }

        public QueryBuilder WhereEndTicketPriceIs(int? maxPrice)
        {
            return WhereSingleOptionIs(EventsParametersConstants.EndTicket, maxPrice.ToString());
        }

        public QueryBuilder WhereTypeOfTicketsIs(TicketType type)
        {
            return WhereSingleOptionIs(EventsParametersConstants.Tickets, type.ToString());
        }

        public string CreateQuery()
        {
            return $"?{string.Join("&", QueryParameters.Where(x => x.Value != null).Select(x => $"{x.Key}={x.Value}"))}";
        }
    }
}
