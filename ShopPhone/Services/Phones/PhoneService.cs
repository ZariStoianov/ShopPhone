namespace ShopPhone.Services.Phones
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
    using ShopPhone.Models.Phones;
    using ShopPhone.Services.Phones.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PhoneService : IPhoneService
    {

        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public PhoneService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public PhoneQueryServiceModel All(string brand = null,
            string searchTerm = null,
            AllPhonesSorting sorting = AllPhonesSorting.DateCreated,
            int currentPage = 1,
            int phonePerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var phoneQuery = this.data.Phones
                .Where(p => !publicOnly || p.IsPublic);

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
                AllPhonesSorting.Price => phoneQuery.OrderBy(p => p.PriceForPhone),
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

        public void ChangeVisility(int phoneId)
        {
            var phone = this.data
                .Phones
                .Find(phoneId);

            phone.IsPublic = !phone.IsPublic;

            this.data.SaveChanges();
        }

        public IEnumerable<string> AllPhoneBrands()
        {
            return this.data
                .Phones
                .Select(b => b.Brand)
                .Distinct()
                .OrderByDescending(br => br)
                .ToList();
        }

        public IEnumerable<PhoneServiceModel> ByUser(string userId)
        {
            return GetPhones(this.data
                .Phones
                .Where(p => p.Owner.UserId == userId));
        }

        private IEnumerable<PhoneServiceModel> GetPhones(IQueryable<Phone> phoneQuery)
        {
            return phoneQuery
                .ProjectTo<PhoneServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        public IEnumerable<PhoneCategoryServiceModel> AllCategories()
        {
            return this.data
                .Categories
                .ProjectTo<PhoneCategoryServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        public PhoneDetailsServiceModel Details(int id)
        {
            return this.data
                .Phones
                .Where(p => p.Id == id)
                .ProjectTo<PhoneDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool CategoryExists(int categoryId)
        {
            return this.data
                .Categories
                .Any(c => c.Id == categoryId);
        }

        public decimal Create(string brand,
            string model,
            string imageUrl,
            string description,
            int year,
            int categoryId,
            decimal price,
            int ownerId)
        {
            var newPhone = new Phone
            {
                Brand = brand,
                Model = model,
                ImageUrl = imageUrl,
                Description = description,
                Year = year,
                CategoryId = categoryId,
                OwnerId = ownerId,
                PriceForPhone = price,
                IsPublic = false
            };

            this.data.Phones.Add(newPhone);
            this.data.SaveChanges();

            return newPhone.Id;
        }

        public bool IsByOwner(int phoneId, int ownerId)
        {
            return this.data
                .Phones
                .Any(p => p.Id == phoneId && p.OwnerId == ownerId);
        }

        public IEnumerable<LatestPhoneServiceModel> Latest()
        {
            return this.data
                .Phones
                .Where(p => p.IsPublic)
                .OrderByDescending(p => p.Id)
                .ProjectTo<LatestPhoneServiceModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();
        }

        public bool Edit(int id, 
            string brand, 
            string model, 
            string imageUrl, 
            string description, 
            int year, 
            int categoryId, 
            decimal price, 
            int ownerId, 
            bool isPublic)
        {
            var phone = this.data
                .Phones
                .Find(id);

            if (phone == null)
            {
                return false;
            }

            phone.Brand = brand;
            phone.Model = model;
            phone.ImageUrl = imageUrl;
            phone.Description = description;
            phone.Year = year;
            phone.CategoryId = categoryId;
            phone.OwnerId = ownerId;
            phone.PriceForPhone = price;
            phone.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }
    }
}
