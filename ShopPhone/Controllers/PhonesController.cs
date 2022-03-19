namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Models;

    public class PhonesController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddPhoneFormModel phone)
        {
            return View();
        }
    }
}
