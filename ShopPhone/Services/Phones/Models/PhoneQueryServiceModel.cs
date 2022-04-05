namespace ShopPhone.Services.Phones.Models
{
    using System.Collections.Generic;

    public class PhoneQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int PhonePerPage { get; set; }

        public int TotalPhones { get; set; }

        public IEnumerable<PhoneServiceModel> Phones { get; set; }
    }
}
