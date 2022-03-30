namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Infrastructure;
    using ShopPhone.Models;
    using ShopPhone.Models.Phones;
    using ShopPhone.Services.Owners;
    using ShopPhone.Services.Phones;

    public class PhonesController : Controller
    {
        private readonly IPhoneService phones;
        private readonly IOwnerService owners;

        public PhonesController(IPhoneService phones, IOwnerService owners)
        {
            this.phones = phones;
            this.owners = owners;
        }

        public IActionResult All([FromQuery] AllPhonesQueryModel query)
        {
            var allPhones = this.phones.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPhonesQueryModel.PhonePerPage);

            var phoneBrand = this.phones.AllPhoneBrands();

            query.TotalPhones = allPhones.TotalPhones;
            query.Brands = phoneBrand;
            query.Phones = allPhones.Phones;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myPhones = this.phones.ByUser(this.User.GetId());

            return View(myPhones);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.owners.IsOwner(this.User.GetId()))
            {
                return RedirectToAction("Create", "Owners");
            }

            return View(new PhoneFormModel
            {
                Categories = this.phones.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PhoneFormModel phone)
        {
            var ownerId = this.owners.GetIdByUser(this.User.GetId());

            if (ownerId == 0)
            {
                return RedirectToAction("Create", "Owners");
            }

            if (!this.phones.CategoryExists(phone.CategoryId))
            {
                this.ModelState.AddModelError(nameof(phone.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                phone.Categories = this.phones.AllCategories();
                return View(phone);
            }

            this.phones.Create(phone.Brand,
                phone.Model,
                phone.ImageUrl,
                phone.Year,
                phone.Description,
                phone.CategoryId,
                ownerId);

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.owners.IsOwner(userId))
            {
                return RedirectToAction("Create", "Owners");
            }

            var phone = this.phones.Details(id);

            if (phone.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new PhoneFormModel
            {
                Brand = phone.Brand,
                Model = phone.Model,
                ImageUrl = phone.ImageUrl,
                Description = phone.Description,
                CategoryId = phone.CategoryId,

                Categories = this.phones.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PhoneFormModel phone)
        {
            var ownerId = this.owners.GetIdByUser(this.User.GetId());

            if (ownerId == 0)
            {
                return RedirectToAction("Create", "Owners");
            }

            if (!this.phones.CategoryExists(phone.CategoryId))
            {
                this.ModelState.AddModelError(nameof(phone.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                phone.Categories = this.phones.AllCategories();
                return View(phone);
            }

            var phoneEdit = this.phones.Edit(id,
                phone.Brand,
                phone.Model,
                phone.ImageUrl,
                phone.Year,
                phone.Description,
                phone.CategoryId,
                ownerId);

            if (!phoneEdit)
            {
                return BadRequest();
            }

            return RedirectToAction("All");
        }
    }
}
