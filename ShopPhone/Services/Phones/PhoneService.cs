namespace ShopPhone.Services.Phones
{
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
    using ShopPhone.Models.Phones;
    using System.Collections.Generic;
    using System.Linq;

    public class PhoneService : IPhoneService
    {

        private readonly ApplicationDbContext data;

        public PhoneService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public PhoneQueryServiceModel All(string brand,
            string searchTerm,
            AllPhonesSorting sorting,
            int currentPage,
            int phonePerPage)
        {
            var phoneQuery = this.data.Phones.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                phoneQuery = phoneQuery
                    .Where(b => b.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                phoneQuery = phoneQuery.Where(p =>
                (p.Brand + " " + p.Model.ToLower()).Contains(searchTerm.ToLower()) ||
                p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            phoneQuery = sorting switch
            {
                AllPhonesSorting.Year => phoneQuery.OrderByDescending(p => p.Year),
                AllPhonesSorting.BrandAndModel => phoneQuery.OrderBy(p => p.Model),
                AllPhonesSorting.DateCreated or _ => phoneQuery.OrderByDescending(p => p.Id),
            };

            var totalPhones = phoneQuery.Count();

            var phones = GetPhones(phoneQuery
                .Skip((currentPage - 1) * phonePerPage)
                .Take(phonePerPage));

            return new PhoneQueryServiceModel
            {
                TotalPhones = totalPhones,
                CurrentPage = currentPage,
                PhonePerPage = phonePerPage,
                Phones = phones
            };
        }

        public IEnumerable<string> AllPhoneBrands()
        {
            return this.data
                .Phones
                .Select(b => b.Brand)
                .Distinct()
                .OrderByDescending(b => b)
                .ToList();
        }

        public IEnumerable<PhoneServiceModel> ByUser(string userId)
        {
            return GetPhones(this.data
                .Phones
                .Where(p => p.Owner.UserId == userId));
        }

        private static IEnumerable<PhoneServiceModel> GetPhones(IQueryable<Phone> phoneQuery)
        {
            return phoneQuery.Select(p => new PhoneServiceModel
            {
                Id = p.Id,
                Brand = p.Brand,
                Model = p.Model,
                Year = p.Year,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.Name
            })
            .ToList();
        }

        public IEnumerable<PhoneCategoryServiceModel> AllCategories()
        {
            return this.data
                .Categories
                .Select(c => new PhoneCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public PhoneDetailsServiceModel Details(int id)
        {
            return this.data
                .Phones
                .Where(p => p.Id == id)
                .Select(p => new PhoneDetailsServiceModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Year = p.Year,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    OwnerId = p.OwnerId,
                    OwnerName = p.Owner.FullName,
                    UserId = p.Owner.UserId
                })
                .FirstOrDefault();
        }

        public bool CategoryExists(int categoryId)
        {
            return this.data
                .Categories
                .Any(c => c.Id == categoryId);
        }

        public int Create(string brand,
            string model,
            string imageUrl,
            int year,
            string description,
            int categoryId,
            int ownerId)
        {
            var phones = new Phone
            {
                Brand = brand,
                Model = model,
                ImageUrl = imageUrl,
                Year = year,
                Description = description,
                CategotyId = categoryId,
                OwnerId = ownerId
            };

            this.data.Add(phones);
            this.data.SaveChanges();

            return phones.Id;
        }

        public bool Edit(int id,
            string brand,
            string model,
            string imageUrl,
            int year,
            string description,
            int categoryId,
            int ownerId)
        {
            var phone = this.data
                .Phones
                .Find(id);

            if (phone.OwnerId != ownerId)
            {
                return false;
            }

            phone.Brand = brand;
            phone.Model = model;
            phone.ImageUrl = imageUrl;
            phone.Year = year;
            phone.Description = description;
            phone.CategotyId = categoryId;
            phone.OwnerId = ownerId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByOwner(int phoneId, int ownerId)
        {
            return this.data
                .Phones
                .Any(p => p.Id == phoneId && p.OwnerId == ownerId);
        }
    }
}
