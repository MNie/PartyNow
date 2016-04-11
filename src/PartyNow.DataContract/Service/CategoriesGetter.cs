using PartyNow.DataContract.Models;

namespace PartyNow.DataContract.Service
{
    public class CategoriesGetter : BaseGetter<Categories>
    {
        public CategoriesGetter(string baseUrl) : base(baseUrl)
        {
        }
    }
}
