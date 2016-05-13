using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PartyNow.DataContract.Service
{
    public interface IBaseGetter<TItem>
    {
        Task<IList<TItem>> Get();
    }

    public abstract class BaseGetter<TItem> : IBaseGetter<TItem>
    {
        protected string _baseUrl;
        protected BaseGetter(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public virtual async Task<IList<TItem>> Get()
        {
            string data;
            using (var wc = new HttpClient() {Timeout = new TimeSpan(0, 0, 2, 0)})
            {
                data = await wc.GetStringAsync(_baseUrl);
            }
            return JsonConvert.DeserializeObject<IList<TItem>>(data);
        }
    }
}
