using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PartyNow.DataContract.Models
{
    public class Attachment
    {
        public string filename { get; set; }
    }

    public class Url
    {
        public string www { get; set; }
        public string tickets { get; set; }
        public string fb { get; set; }
    }

    public class Ticket
    {

        public string type { get; set; }
        public string startTicket { get; set; }
        public string endTicket { get; set; }

        public override string ToString()
        {
            return $"{type} od {startTicket} do {endTicket}";
        }
    }

    public class Events
    {
        public int? id { get; set; }
        [JsonProperty(PropertyName = "place")]
        public Places miejsce { get; set; }
        [JsonProperty(PropertyName = "endDate")]
        public DateTime? koniec { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string nazwa { get; set; }
        public Url urls { get; set; }
        public IEnumerable<Attachment> attachments { get; set; }
        public string descLong { get; set; }
        public int categoryId { get; set; }
        [JsonProperty(PropertyName = "startDate")]
        public DateTime? start { get; set; }
        [JsonProperty(PropertyName = "organizer")]
        public Organizers organizator { get; set; }
        [JsonProperty(PropertyName = "active")]
        public int? aktualny { get; set; }
        [JsonProperty(PropertyName = "tickets")]
        public Ticket bilet { get; set; }
        public string descShort { get; set; }

        public override string ToString()
        {
            return $"{start} - {koniec} {nazwa}";
        }

        public override bool Equals(object obj)
        {
            return id.Equals((obj as Events).id);
        }
    }


}
