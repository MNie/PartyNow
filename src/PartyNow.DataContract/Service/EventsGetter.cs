using PartyNow.DataContract.Models;

namespace PartyNow.DataContract.Service
{
    public class EventsGetter : RichGetter<Events>
    {
        public EventsGetter(string baseUrl) : base(baseUrl)
        {
        }
    }
}
