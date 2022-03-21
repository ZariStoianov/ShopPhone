namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
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

        public IActionResult Add()
        {
            return View(new AddPhoneFormModel
            {
                Categories = this.GetCategory()
            });
        }

        public IActionResult All()
        {
            var phones = this.data
                .Phones
                .OrderByDescending(p => p.Id)
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

            return View(new AllPhonesQueryModel 
            {
                Phones = phones
            });
        }

        [HttpPost]
        public IActionResult Add(AddPhoneFormModel phone)
        {
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
                CategotyId = phone.CategoryId
            };

            this.data.Add(phones);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
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
