using System.Collections.Generic;

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
    }

    public class Events
    {
        public int? id { get; set; }
        public Places place { get; set; }
        public string endDate { get; set; }
        public string name { get; set; }
        public Url urls { get; set; }
        public IEnumerable<Attachment> attachments { get; set; }
        public string descLong { get; set; }
        public int categoryId { get; set; }
        public string startDate { get; set; }
        public Organizers organizer { get; set; }
        public int? active { get; set; }
        public Ticket tickets { get; set; }
        public string descShort { get; set; }
    }


}
