namespace ShopPhone.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PhonesController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
