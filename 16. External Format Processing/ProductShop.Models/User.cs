using System.Collections.Generic;

namespace ProductShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }

        public ICollection<Product> BoughtProducts { get; set; } = new List<Product>();

        public ICollection<Product> ProductsForSale { get; set; } = new List<Product>();
    }
}
