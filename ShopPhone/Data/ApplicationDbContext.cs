namespace ShopPhone.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ShopPhone.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Phone>()
                .HasOne(o => o.Owner)
                .WithMany(p => p.Phones)
                .HasForeignKey(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Phone>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Phones)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Owner>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Owner>(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Phone>()
                .Property(p => p.PriceForPhone)
                .HasPrecision(18, 2);

            base.OnModelCreating(builder);
        }
    }
}
