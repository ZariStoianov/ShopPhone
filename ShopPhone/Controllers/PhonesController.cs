namespace ShopPhone.Controllers
{
    using AutoMapper;
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
        private readonly IMapper mapper;

        public PhonesController(IPhoneService phones,
            IOwnerService owners,
            IMapper mapper)
        {
            this.phones = phones;
            this.owners = owners;
            this.mapper = mapper;
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

        public IActionResult Details(int id, string information)
        {
            var phone = this.phones.Details(id);

            if (information != phone.ToFriendlyUrl())
            {
                return BadRequest();
            }

            return View(phone);
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

            var phoneId = this.phones.Create(phone.Brand,
                phone.Model,
                phone.ImageUrl,
                phone.Year,
                phone.Description,
                phone.CategoryId,
                ownerId);

            TempData[GlobalMessageKey] = "You phone was added and is awaiting for approval.";

            return RedirectToAction(nameof(Details), new { id = phoneId, information = phone.ToFriendlyUrl()});
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.owners.IsOwner(userId) && !User.IsAdmin())
            {
                return RedirectToAction("Create", "Owners");
            }

            var phone = this.phones.Details(id);

            if (phone.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var phoneForm = this.mapper.Map<PhoneFormModel>(phone);

            phoneForm.Categories = this.phones.AllCategories();

            return View(phoneForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PhoneFormModel phone)
        {
            var ownerId = this.owners.GetIdByUser(this.User.GetId());

            if (ownerId == 0 && !User.IsAdmin())
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

            if (!this.phones.IsByOwner(id, ownerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

             var edited = this.phones.Edit(id,
                phone.Brand,
                phone.Model,
                phone.ImageUrl,
                phone.Year,
                phone.Description,
                phone.CategoryId,
                ownerId);


            TempData[GlobalMessageKey] = $"You phone was edited{(this.User.IsAdmin() ? string.Empty : " and is awaiting for approval")}!";

            return RedirectToAction(nameof(Details), new { id, information = phone.ToFriendlyUrl() });
        }
    }
}
