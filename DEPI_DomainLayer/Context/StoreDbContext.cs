using DEPI_DomainLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_DomainLayer.Context
{
    public class StoreDbContext :IdentityDbContext<ApplicationUser>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Order and OrderItem
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems) // Each Order can have many OrderItems
                .WithOne(oi => oi.Order) // Each OrderItem belongs to one Order
                .HasForeignKey(oi => oi.OrderId) // Specify the foreign key
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            // Configure the relationship between OrderItem and Product
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product) // Each OrderItem has one Product
                .WithMany() // A Product can be in many OrderItems
                .HasForeignKey(oi => oi.ProductId) // Specify the foreign key
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior

            modelBuilder.Entity<OrderItem>()
           .Property(oi => oi.Price)
           .HasColumnType("decimal(18, 2)");

            // call FLuent Api (Call All Class that implement IEntityTypeConfigration)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //call all class that implement IEntityTypeConfiguration

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
