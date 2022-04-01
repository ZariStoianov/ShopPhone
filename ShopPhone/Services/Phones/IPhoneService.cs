namespace ShopPhone.Services.Phones
{
    using ShopPhone.Models.Phones;
    using ShopPhone.Services.Phones.Models;
    using System.Collections.Generic;

    public interface IPhoneService
    {
        PhoneQueryServiceModel All(string brand,
            string searchTerm,
            AllPhonesSorting sorting,
            int currentPage,
            int phonePerPage);

        IEnumerable<string> AllPhoneBrands();

        IEnumerable<PhoneServiceModel> ByUser(string userId);

        IEnumerable<PhoneCategoryServiceModel> AllCategories();

        bool IsByOwner(int phoneId, int ownerId);

        bool CategoryExists(int categoryId);

        int Create(string brand,
                string model,
                string imageUrl,
                int year,
                string description,
                int categoryId,
                int ownerId);

        bool Edit(int id,
                string brand,
                string model,
                string imageUrl,
                int year,
                string description,
                int categoryId,
                int ownerId);

        PhoneDetailsServiceModel Details(int id);
    }
}
