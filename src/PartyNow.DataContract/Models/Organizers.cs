namespace PartyNow.DataContract.Models
{
    public class Organizers
    {
        public int? id { get; set; }
        public string designation { get; set; }
        public override string ToString()
        {
            return designation;
        }

        public override bool Equals(object obj)
        {
            return id.Equals((obj as Organizers).id);
        }
    }
}
