using System;
using System.Collections.Generic;
using PartyNow.DataContract.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PartyNow.DataContract.Service
{
    public class EventsGetter
    {
        private readonly string _baseUrl;
        public EventsGetter(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<IEnumerable<Events>> Get()
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var query = $"{_baseUrl}?start_date={today}";
            var wc = new HttpClient();
            return JsonConvert.DeserializeObject<IEnumerable<Events>>(await wc.GetStringAsync(query));
        } 
    }
}
