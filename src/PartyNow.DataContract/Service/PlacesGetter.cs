using PartyNow.DataContract.Models;

namespace PartyNow.DataContract.Service
{
    public class PlacesGetter : BaseGetter<Places>
    {
        public PlacesGetter(string baseUrl) : base(baseUrl)
        {
        }
    }
}
