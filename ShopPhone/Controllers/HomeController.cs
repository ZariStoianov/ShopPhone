namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Data;
    using ShopPhone.Models;
    using ShopPhone.Models.Home;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext data;

        public HomeController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalPhones = this.data.Phones.Count();

            var phones = this.data
                .Phones
                .OrderByDescending(p => p.Id)
                .Select(p => new PhoneIndexViewModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Year = p.Year,
                    ImageUrl = p.ImageUrl
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {
                TotalPhones = totalPhones,
                Phones = phones
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
