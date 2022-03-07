using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF
{
    public class OrderContext : DbContext
    {
            public DbSet<Order> Orders { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<OrderHistory> OrdersHistory { get; set; }
            public DbSet<State> State { get; set; }
            public DbSet<OrderProducts> OrderProducts { get; set; }
            public DbSet<OrderProductsHistory> OrderProductsHistory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(c => c.Products)
                .WithMany(s => s.Orders)
                .UsingEntity<OrderProducts>(j => j
                   .HasOne(pt => pt.Product)
                   .WithMany(t => t.OrderProducts)
                   .HasForeignKey(pt => pt.ProductId),
                j => j
                    .HasOne(pt => pt.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(pt => pt.OrderId),
                j =>
                {
                    j.HasKey(t => new { t.ProductId, t.OrderId });
                    j.ToTable("OrderProducts");
                });
        }
        public OrderContext(DbContextOptions<OrderContext> options)
           : base(options)
        {
        }

    }
}
