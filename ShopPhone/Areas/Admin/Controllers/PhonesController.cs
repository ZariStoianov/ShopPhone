namespace ShopPhone.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Services.Phones;

    public class PhonesController : AdminController
    {

        private readonly IPhoneService phone;

        public PhonesController(IPhoneService phone)
        {
            this.phone = phone;
        }

        public IActionResult All()
        {
            var allPhones = this.phone
                 .All(publicOnly: false)
                 .Phones;

            return View(allPhones);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.phone.ChangeVisility(id);

            return RedirectToAction("All");
        }
    }
}
