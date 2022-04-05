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
            var allPhone = this.phone
                 .All(publicOnly: false)
                 .Phones;

            return View(allPhone);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.phone.ChangeVisility(id);

            return RedirectToAction("All");
        }
    }
}
