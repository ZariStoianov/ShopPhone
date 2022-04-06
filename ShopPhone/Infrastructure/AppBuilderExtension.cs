using ShopPhone.Areas.Admin;

namespace ShopPhone.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ShopPhone.Data;
    using ShopPhone.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static AdminConstants;

    public static class AppBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();
            var services = scopedService.ServiceProvider;
            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);
            return app;
        }

        private static void MigrateDatabase(IServiceProvider service)
        {
            var data = service.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider service)
        {

            var data = service.GetRequiredService<ApplicationDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{Name = "iOS"},
                new Category{Name = "Android"},
                new Category{Name = "Linux"},
                new Category{Name = "Windows Phone"},
                new Category{Name = "Amazon's Fire Phone"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async() =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }
                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "babo4ko@gmail.com";
                const string adminPass = "123456";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPass);

                await userManager.AddToRoleAsync(user, role.Name);
            })
                 .GetAwaiter()
                 .GetResult();
        }
    }
}
