namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
    using ShopPhone.Infrastructure;
    using ShopPhone.Models;
    using ShopPhone.Models.Phones;
    using System.Collections.Generic;
    using System.Linq;

    public class PhonesController : Controller
    {
        private readonly ApplicationDbContext data;

        public PhonesController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult All([FromQuery]AllPhonesQueryModel query)
        {
            var phoneQuery = this.data.Phones.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                phoneQuery = phoneQuery
                    .Where(b => b.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                phoneQuery = phoneQuery.Where(p =>
                (p.Brand + " " + p.Model.ToLower()).Contains(query.SearchTerm.ToLower()) ||
                p.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            phoneQuery = query.Sorting switch
            {
                AllPhonesSorting.Year => phoneQuery.OrderByDescending(p => p.Year),
                AllPhonesSorting.BrandAndModel => phoneQuery.OrderBy(p => p.Model),
                AllPhonesSorting.DateCreated or _ => phoneQuery.OrderByDescending(p => p.Id),
            };

            var totalPhones = phoneQuery.Count();

            var phones = phoneQuery
                .Skip((query.CurrentPage - 1) * AllPhonesQueryModel.PhonePerPage)
                .Take(AllPhonesQueryModel.PhonePerPage)
                .Select(p => new PhoneListingViewModel
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

            var phoneBrand = this.data
                .Phones
                .Select(b => b.Brand)
                .Distinct()
                .OrderByDescending(b => b)
                .ToList();

            query.TotalPhones = totalPhones;
            query.Brands = phoneBrand;
            query.Phones = phones;

            return View(query);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!UserIsOwner())
            {
                return RedirectToAction("Create", "Owners");
            } 

            return View(new AddPhoneFormModel
            {
                Categories = this.GetCategory()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPhoneFormModel phone)
        {
            var ownerId = this.data
                .Owners
                .Where(o => o.UserId == this.User.GetId())
                .Select(o => o.Id)
                .FirstOrDefault();

            if (ownerId == 0)
            {
                return RedirectToAction("Create", "Owners");
            }

            if (!this.data.Categories.Any(c => c.Id == phone.CategoryId))
            {
                this.ModelState.AddModelError(nameof(phone.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                phone.Categories = this.GetCategory();
                return View(phone);
            }

            var phones = new Phone
            {
                Brand = phone.Brand,
                Model = phone.Model,
                ImageUrl = phone.ImageUrl,
                Year = phone.Year,
                Description = phone.Description,
                CategotyId = phone.CategoryId,
                OwnerId = ownerId
            };

            this.data.Add(phones);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public bool UserIsOwner()
        {
            return data
                .Owners
                .Any(o => o.UserId == this.User.GetId());
        }

        private IEnumerable<PhoneCategoryFormModel> GetCategory()
        {
            return this.data
                .Categories
                .Select(c => new PhoneCategoryFormModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }
    }
}
