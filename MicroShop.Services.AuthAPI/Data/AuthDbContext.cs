using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MircroShop.Services.AuthAPI.Tables;

namespace MircroShop.Services.AuthAPI.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Coupon>().HasIndex(c => c.Code).IsUnique();

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Coupon>().HasData(new Coupon(1, "10OFF", 10.00M, false, 1000.00M, 20.00M, false, 5, 0, new DateTime(2025, 07, 25, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 08, 25, 0, 0, 0, DateTimeKind.Utc)));
            //modelBuilder.Entity<Coupon>().HasData(new Coupon(2, "13OFF", 10.00M, false, 1000.00M, 20.00M, false, 5, 0, new DateTime(2025, 07, 25, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 08, 25, 0, 0, 0, DateTimeKind.Utc)));
            //modelBuilder.Entity<Coupon>().HasData(new Coupon(3, "10OAA", 10.00M, false, 1000.00M, 20.00M, false, 5, 0, new DateTime(2025, 07, 25, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 08, 25, 0, 0, 0, DateTimeKind.Utc)));
            //modelBuilder.Entity<Coupon>().HasData(new Coupon(4, "10OBB", 05.00M, true, 1000.00M, 20.00M, false, 5, 0, new DateTime(2025, 07, 25, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 08, 25, 0, 0, 0, DateTimeKind.Utc)));
            //modelBuilder.Entity<Coupon>().HasData(new Coupon(5, "10OCC", 10.00M, true, 1000.00M, 20.00M, false, 5, 0, new DateTime(2025, 07, 25, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 08, 25, 0, 0, 0, DateTimeKind.Utc)));
        }
    }
}
