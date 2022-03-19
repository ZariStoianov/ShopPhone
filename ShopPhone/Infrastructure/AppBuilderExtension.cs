namespace ShopPhone.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
    using System.Linq;

    public static class AppBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopeService = app.ApplicationServices.CreateScope();

            var data = scopeService.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            return app;
        }

        private static void SeedCategories(ApplicationDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{Name = "iPhone"},
                new Category{Name = "Android"},
                new Category{Name = "Windows Phone"},
                new Category{Name = "Amazon's Fire Phone"},
            });

            data.SaveChanges();
        }
    }
}
