namespace PartyNow.DataContract.Models
{
    public class Address
    {
        public string street { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
    public class Places
    {
        public int? id { get; set; }
        public Address address { get; set; }
        public string name { get; set; }
        public string subname { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
