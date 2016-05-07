﻿using System;
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

        public override async Task<IList<Events>> Get()
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var query = $"{_baseUrl}?start_date={today}";
            using (var wc = new HttpClient() {Timeout = new TimeSpan(0, 0, 2, 0)})
            {
                return JsonConvert.DeserializeObject<IList<Events>>(await wc.GetStringAsync(query));
            }
        }

        public async /*Task<IEnumerable<Events>>*/ Task<string> GetBasedOnQuery(string baseQuery)
        {
            var query = $"{_baseUrl}{baseQuery}";
            using (var wc = new HttpClient())
            {
                return await wc.GetStringAsync(query);
            }
        }
    }
}
