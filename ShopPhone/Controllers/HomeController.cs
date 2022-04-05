namespace ShopPhone.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using ShopPhone.Services.Phones;
    using ShopPhone.Services.Phones.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IPhoneService phone;
        private readonly IMemoryCache cache;

        public HomeController(IMemoryCache cache, IPhoneService phone)
        {
            this.cache = cache;
            this.phone = phone;
        }

        public IActionResult Index()
        {
            const string latestPhonesCacheKey = "LatestPhonesCacheKey";

            var phoneCache = this.cache.Get<List<LatestPhoneServiceModel>>(latestPhonesCacheKey);

            if (phoneCache == null)
            {
                phoneCache = this.phone
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(latestPhonesCacheKey, phoneCache, cacheOptions);
            }

            return View(phoneCache);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
