using System;
using System.Globalization;
using System.Numerics;
using better.Entities;
using Microsoft.EntityFrameworkCore;

namespace kolos2.Entities
{
    public class AppDbContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders  { get; set; }
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<Product> Products { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Product 1", Price = 10.54 }, new Product { Id = 2, Name = "Product 2", Price = 145.90 });

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, FirstName = "John", LastName = "Doe" },
                new Client { Id = 2, FirstName = "Arnold", LastName = "Boe" },
                new Client { Id = 3, FirstName = "Cristof", LastName = "Zoe" }
             );

            modelBuilder.Entity<Status>().HasData(new Status { Id = 1, Name = "Created" }, new Status { Id = 2, Name = "Finished" });
            modelBuilder.Entity<Order>().HasData(
                new Order { ClientId = 1,
                    StatusId = 1,
                    CreatedAt = DateTime.Parse("2023-06-25 10:30:00", CultureInfo.InvariantCulture),
                    FullfiledAt = DateTime.Parse("2023-06-28 10:30:00", CultureInfo.InvariantCulture),
                    Id =1
                },
                new Order
                {
                    ClientId = 2,
                    StatusId = 1,
                    CreatedAt = DateTime.Parse("2023-06-25 10:30:00", CultureInfo.InvariantCulture),
                    Id = 2
                }
              );
            modelBuilder.Entity<ProductOrder>().HasData(new ProductOrder { ProductId = 1, OrderId = 1, Amount = 3 });


        }
    }
}

