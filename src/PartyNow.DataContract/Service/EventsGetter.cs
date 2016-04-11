using System;
using System.Collections.Generic;
using PartyNow.DataContract.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PartyNow.DataContract.Service
{
    public class EventsGetter : BaseGetter<Events>
    {
        public EventsGetter(string baseUrl) : base(baseUrl) 
        {
        }

        public override async Task<IEnumerable<Events>> Get()
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var query = $"{_baseUrl}?start_date={today}";
            var wc = new HttpClient();
            return JsonConvert.DeserializeObject<IEnumerable<Events>>(await wc.GetStringAsync(query));
        } 
    }
}
