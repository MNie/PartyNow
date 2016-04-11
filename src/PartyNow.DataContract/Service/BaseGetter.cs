using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PartyNow.DataContract.Service
{
    public abstract class BaseGetter<TItem>
    {
        protected string _baseUrl;
        protected BaseGetter(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public virtual async Task<IEnumerable<TItem>> Get()
        {
            using (var wc = new HttpClient())
            {
                return JsonConvert.DeserializeObject<IEnumerable<TItem>>(await wc.GetStringAsync(_baseUrl));
            }
        }
    }
}
