namespace ShopPhone.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalPhones { get; set; }

        public int TotalUsers { get; set; }

        public int TotalPrice { get; set; }

        public List<PhoneIndexViewModel> Phones { get; set; }
    }
}
