using System.Collections.Generic;

namespace ShopPhone.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Phone> Phones { get; set; } = new List<Phone>();
    }
}
