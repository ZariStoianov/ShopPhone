namespace ShopPhone.Test.Data
{
    using ShopPhone.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Phones
    {
        public static IEnumerable<Phone> TenPublicPhones()
        {
            return Enumerable.Range(0, 10).Select(p => new Phone
            {
                IsPublic = true
            });
        }
    }
}
