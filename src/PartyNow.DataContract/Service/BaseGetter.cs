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

        public virtual async Task<IList<TItem>> Get()
        {
            using (var wc = new HttpClient())
            {
                return JsonConvert.DeserializeObject<IList<TItem>>(await wc.GetStringAsync(_baseUrl));
            }
        }
    }
}
