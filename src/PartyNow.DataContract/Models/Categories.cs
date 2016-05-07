namespace PartyNow.DataContract.Models
{
    public class Parent
    {
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class Categories
    {
        public int? id { get; set; }
        public string name { get; set; }
        public Parent root_category { get; set; }
        public Parent parent { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
