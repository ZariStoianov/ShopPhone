namespace ShopPhone.Services.Phones
{
    using ShopPhone.Models.Phones;
    using ShopPhone.Services.Phones.Models;
    using System.Collections.Generic;

    public interface IPhoneService
    {
        PhoneQueryServiceModel All(string brand = null,
            string searchTerm = null,
            AllPhonesSorting sorting = AllPhonesSorting.DateCreated,
            int currentPage = 1,
            int phonePerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<string> AllPhoneBrands();

        IEnumerable<PhoneServiceModel> ByUser(string userId);

        IEnumerable<PhoneCategoryServiceModel> AllCategories();

        IEnumerable<LatestPhoneServiceModel> Latest();

        void ChangeVisility(int carId);

        bool IsByOwner(int phoneId, int ownerId);

        bool CategoryExists(int categoryId);

        decimal Create(string brand,
                string model,
                string imageUrl,
                string description,
                int year,
                int categoryId,
                decimal price,
                int ownerId);

        bool Edit(int id,
                string brand,
                string model,
                string imageUrl,
                string description,
                int year,
                int categoryId,
                decimal price,
                int ownerId,
                bool isPublic);

        int Delete(int id);

        PhoneDetailsServiceModel Details(int id);
    }
}
