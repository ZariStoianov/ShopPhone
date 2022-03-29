namespace ShopPhone.Services.Phones
{
    using ShopPhone.Models.Phones;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPhoneService
    {
        PhoneQueryServiceModel All(string brand,
            string searchTerm,
            AllPhonesSorting sorting,
            int currentPage,
            int phonePerPage);
    }
}
