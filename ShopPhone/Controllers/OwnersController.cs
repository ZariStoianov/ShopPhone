namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
    using ShopPhone.Infrastructure;
    using ShopPhone.Models.Owners;
    using System.Linq;

    public class OwnersController : Controller
    {
        private readonly ApplicationDbContext data;

        public OwnersController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeOwnerFormModel owner)
        {
            var userId = this.User.GetId();

            var userIsAlreadyOwner = this.data
                .Owners
                .Any(o => o.UserId == userId);

            if (userIsAlreadyOwner)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(owner);
            }

            var owners = new Owner
            {
                FullName = owner.Name,
                PhoneNumber = owner.PhoneNumber,
                UserId = userId
            };

            this.data.Owners.Add(owners);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becomming a owner!";

            return RedirectToAction(nameof(PhonesController.All), "Phones");
        }
    }
}
