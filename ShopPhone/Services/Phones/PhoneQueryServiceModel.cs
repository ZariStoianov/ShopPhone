namespace ShopPhone.Services.Phones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PhoneQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int PhonePerPage { get; set; }

        public int TotalPhones { get; set; }

        public IEnumerable<PhoneServiceModel> Phones { get; set; }
    }
}
