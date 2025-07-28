using MicroShop.Services.EmailAPI.Tables;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Services.EmailAPI.Data
{
    public class EmailDbContext : DbContext
    {

        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {
        }


        public DbSet<EmailLog> EmailLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

      
    }
}
