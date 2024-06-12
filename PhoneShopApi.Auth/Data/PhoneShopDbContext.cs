using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Microsoft.AspNetCore.Http;
using Azure.Core;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Data
{
    public class PhoneShopDbContext : IdentityDbContext<User>
    {
        public PhoneShopDbContext(
            DbContextOptions<PhoneShopDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator == null) return;

                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PhoneDetail> PhoneDetails { get; set; } = null!;
        public DbSet<PhoneOption> PhoneOptions { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<PhoneColor> PhoneColors { get; set; } = null!;
        public DbSet<BuiltInStorage> BuiltInStorages { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

    }
}
