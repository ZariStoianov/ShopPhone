namespace ShopPhone.Services.Phones
{
    using ShopPhone.Data;
    using ShopPhone.Models.Phones;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

            var phones = phoneQuery
                .Skip((currentPage - 1) * phonePerPage)
                .Take(phonePerPage)
                .Select(p => new PhoneServiceModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Year = p.Year,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    Category = p.Category.Name
                })
                .ToList();

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
    }
}
