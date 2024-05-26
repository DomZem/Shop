using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence
{
    public class ShopDbContext(DbContextOptions<ShopDbContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<ProductCategory> ProductCategories { get; set; }
        
        internal DbSet<OrderStatus> OrderStatuses { get; set; }
        
        internal DbSet<Product> Products { get; set; }
        
        internal DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.OrderAddress);

            modelBuilder.Entity<ProductCategory>()
                .HasIndex(pc => pc.Name)
                .IsUnique();

            modelBuilder.Entity<OrderStatus>()
                .HasIndex(os => os.Name)
                .IsUnique();

            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId);

            modelBuilder.Entity<OrderStatus>()
                .HasMany(os => os.Orders)
                .WithOne(o => o.OrderStatus)
                .HasForeignKey(p => p.OrderStatusId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.OrderedBy)
                .HasForeignKey(o => o.OrderedById);
        }
    }
}
