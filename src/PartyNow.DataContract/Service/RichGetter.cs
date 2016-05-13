using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PartyNow.DataContract.Models;

namespace PartyNow.DataContract.Service
{
    public interface IRichGetter<TItem> : IBaseGetter<TItem>
    {
        Task<IList<TItem>> GetBasedOnQuery(string baseQuery);
    }

    public abstract class RichGetter<TItem> : IRichGetter<TItem>
    {
        protected string _baseUrl;
        protected RichGetter(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public async Task<IList<TItem>> Get()
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var query = $"{_baseUrl}?start_date={today}";
            using (var wc = new HttpClient() { Timeout = new TimeSpan(0, 0, 2, 0) })
            {
                return JsonConvert.DeserializeObject<IList<TItem>>(await wc.GetStringAsync(query));
            }
        }

        public async Task<IList<TItem>> GetBasedOnQuery(string baseQuery)
        {
            var query = $"{_baseUrl}{baseQuery}";
            using (var wc = new HttpClient())
            {
                return JsonConvert.DeserializeObject<IList<TItem>>(await wc.GetStringAsync(query));
            }
        }

    }
}
