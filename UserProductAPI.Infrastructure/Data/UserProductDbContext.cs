using Microsoft.EntityFrameworkCore;
using UserProductAPI.Core.Entities;

namespace UserProductAPI.Infrastructure.Data
{
    public class UserProductDbContext : DbContext
    {
        public UserProductDbContext(DbContextOptions<UserProductDbContext> options)
       : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)");
            });
        }
    }
}
