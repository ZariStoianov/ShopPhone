namespace ShopPhone.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public HomeController(ApplicationDbContext data, IStatisticsService statistics, IMapper mapper)
        {
            this.data = data;
            this.statistics = statistics;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {

            var phones = this.data
                .Phones
                .OrderByDescending(p => p.Id)
                .ProjectTo<PhoneIndexViewModel>(this.mapper.ConfigurationProvider)
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
