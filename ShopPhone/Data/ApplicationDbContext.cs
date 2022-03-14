namespace ShopPhone.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ShopPhone.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Owner> Owners { get; set; }
    }
}
