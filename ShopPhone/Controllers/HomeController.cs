namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Data;
    using ShopPhone.Models;
    using ShopPhone.Models.Home;
    using ShopPhone.Services.Statistics;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(ApplicationDbContext data, IStatisticsService statistics)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {

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

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalPhones = totalStatistics.TotalPhones,
                TotalUsers = totalStatistics.TotalUsers,
                Phones = phones
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
