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

        private void AddDataToRoleTable(ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole>
            {
                new()
                {
                    Name ="Admin",
                    NormalizedName = "ADMIN"
                },                
                new()
                {
                    Name ="Staff",
                    NormalizedName = "STAFF"
                },
                new()
                {
                    Name ="User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }


        private void AddDataToBrandTable(ModelBuilder modelBuilder)
        {
            var brands = new List<Brand>
            {
                new()
                {
                    Id = 1,
                    Name = "Apple"
                },
                new()
                {
                    Id = 2,
                    Name = "Samsung"
                },
                new()
                {
                    Id = 3,
                    Name = "Google"
                },
            };
            modelBuilder.Entity<Brand>().HasData(brands);
        }

        private void AddDataToBuiltInStorageTable(ModelBuilder modelBuilder)
        {
            var builtInStorages = new List<BuiltInStorage>
            {
                new()
                {//1
                    Id = 1,
                    Capacity = 128,
                    Unit = "GB"
                },
                new()
                {//2
                    Id = 2,
                    Capacity = 256,
                    Unit = "GB"
                },
                new()
                {//3
                    Id = 3,
                    Capacity = 512,
                    Unit = "GB"
                },
                new()
                {//4
                    Id = 4,
                    Capacity = 1,
                    Unit = "T"
                },
            };
            modelBuilder.Entity<BuiltInStorage>().HasData(builtInStorages);
        }

        private void AddDataToPhoneTable(ModelBuilder modelBuilder)
        {
            var iPhones = new List<Phone>
            {
                new()
                {//1
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {//2
                    Id = 2,
                    Name = "iPhone 15 Pro",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {//3
                    Id = 3,
                    Name = "iPhone 14 Pro Max",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {//4
                    Id = 4,
                    Name = "iPhone 14 Pro",
                    IsSelling = true,
                    BrandId = 1,
                },
            };
            modelBuilder.Entity<Phone>().HasData(iPhones);
            var samsungs = new List<Phone>
            {
                new()
                {
                    Id = 5,
                    Name = "Galaxy S24",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 6,
                    Name = "Galaxy S24 Ultra",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 7,
                    Name = "Galaxy S24 Plus",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 8,
                    Name = "Galaxy S22 Ultra",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 9,
                    Name = "Galaxy S21 Ultra",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 10,
                    Name = "Galaxy Note 10 Plus",
                    IsSelling = true,
                    BrandId = 2,
                },
            };
            modelBuilder.Entity<Phone>().HasData(samsungs);
            var googles = new List<Phone>
            {
                new()
                {
                    Id = 11,
                    Name = "Pixel 8 Pro 5G",
                    BrandId = 3,
                },
                new()
                {
                    Id = 12,
                    Name = "Pixel 8",
                    BrandId = 3,
                },
                new()
                {
                    Id = 13,
                    Name = "Pixel Fold",
                    BrandId = 3,
                },
                new()
                {
                    Id = 14,
                    Name = "Pixel 7",
                    BrandId = 3,
                },
                new()
                {
                    Id = 15,
                    Name = "Pixel 7a",
                    BrandId = 3,
                },
                new()
                {
                    Id = 16,
                    Name = "Pixel 7 Pro",
                    BrandId = 3,
                },
            };
            modelBuilder.Entity<Phone>().HasData(googles);
        }

        private void AddDataToPhoneDetailTable(ModelBuilder modelBuilder)
        {
            var iPhonesDetail = new List<PhoneDetail>
            {
                new()
                {//1
                    Id = 1,
                    PhoneId = 1
                },
                new()
                {//2
                    Id = 2,
                    PhoneId = 2
                },
                new()
                {//3
                    Id = 3,
                    PhoneId = 3
                },
                new()
                {//4
                    Id = 4,
                    PhoneId = 4
                },
            };
            modelBuilder.Entity<PhoneDetail>().HasData(iPhonesDetail);
            var samsungsDetail = new List<PhoneDetail>
            {
                new()
                {
                    Id = 5,
                    PhoneId = 5
                },
                new()
                {
                    Id = 6,
                    PhoneId = 6
                },
                new()
                {
                    Id = 7,
                    PhoneId = 7
                },
                new()
                {
                    Id = 8,
                    PhoneId = 8
                },
                new()
                {
                    Id = 9,
                    PhoneId = 9
                },
                new()
                {
                    Id = 10,
                    PhoneId = 10
                },
            };
            modelBuilder.Entity<PhoneDetail>().HasData(samsungsDetail);
            var googlesDetail = new List<PhoneDetail>
            {
                new()
                {
                    Id = 11,
                    PhoneId = 11
                },
                new()
                {
                    Id = 12,
                    PhoneId = 12
                },
                new()
                {
                    Id = 13,
                    PhoneId = 13
                },
                new()
                {
                    Id = 14,
                    PhoneId = 14
                },
                new()
                {
                    Id = 15,
                    PhoneId = 15
                },
                new()
                {
                    Id = 16,
                    PhoneId = 16
                },
            };
            modelBuilder.Entity<PhoneDetail>().HasData(googlesDetail);
        }



        private void AddDataToPhoneColorTable(ModelBuilder modelBuilder)
        {
            var hostUrl = "http://14.225.207.131:19001";
            var iPhone15ProMax256Colors = new List<PhoneColor>
            {
                new()
                {
                    Id = 1,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_den.webp",
                },
                new()
                {//2
                    Id = 2,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_trang.webp",
                },
                new()
                {//3
                    Id = 3,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_xanh.webp",
                },
                new()
                {//4
                    Id = 4,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_titan.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone15ProMax256Colors);
            var iPhone15ProMax512Colors = new List<PhoneColor>
            {
                new()
                {//5
                    Id = 5,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_den.webp",
                },
                new()
                {//6
                    Id = 6,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_trang.webp",
                },
                new()
                {//7
                    Id = 7,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_xanh.webp",
                },
                new()
                {//8
                    Id = 8,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_titan.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone15ProMax512Colors);
            var iPhone15Pro512Colors = new List<PhoneColor>
            {
                new()
                {//9
                    Id = 9,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_den.webp",
                },
                new()
                {//10
                    Id = 10,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_trang.webp",
                },
                new()
                {//11
                    Id = 11,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_xanh.webp",
                },
                new()
                {//12
                    Id = 12,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_titan.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone15Pro512Colors);
            var iPhone15Pro256Colors = new List<PhoneColor>
            {
                new()
                {//13
                    Id = 13,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_den.webp",
                },
                new()
                {//14
                    Id = 14,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_trang.webp",
                },
                new()
                {//15
                    Id = 15,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_xanh.webp",
                },
                new()
                {//16
                    Id = 16,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_titan.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone15Pro256Colors);
            var iPhone15Pro128Colors = new List<PhoneColor>
            {
                new()
                {//17
                    Id = 17,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_den.webp",
                },
                new()
                {//18
                    Id = 18,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_trang.webp",
                },
                new()
                {//19
                    Id = 19,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_xanh.webp",
                },
                new()
                {//20
                    Id = 20,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pr_titan.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone15Pro128Colors);
            var iPhone14ProMax256Colors = new List<PhoneColor>
            {
                new()
                {//21
                    Id = 21,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-spaceblack.webp",
                },
                new()
                {//22
                    Id = 22,
                    Name = "Bạc",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-silver.webp",
                },
                new()
                {//23
                    Id = 23,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-gold.webp",
                },
                new()
                {//24
                    Id = 24,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-deeppurple.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone14ProMax256Colors);
            var iPhone14Pro256Colors = new List<PhoneColor>
            {
                new()
                {//25
                    Id = 25,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-spaceblack.webp",
                },
                new()
                {//26
                    Id = 26,
                    Name = "Bạc",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-silver.webp",
                },
                new()
                {//27
                    Id = 27,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-gold.webp",
                },
                new()
                {//28
                    Id = 28,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-deeppurple.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(iPhone14Pro256Colors);
            var s24 = new List<PhoneColor>
            {
                new()
                {
                    Id = 29,
                    Name = "Bạc",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_bac.webp",
                },
                new()
                {//2
                    Id = 30,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_den.webp",
                },
                new()
                {//3
                    Id = 31,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_tim.webp",
                },
                new()
                {//4
                    Id = 32,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_vang.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(s24);
            var s24u = new List<PhoneColor>
            {
                new()
                {
                    Id = 33,
                    Name = "Xám",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_xam.webp",
                },
                new()
                {//2
                    Id = 34,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_den.webp",
                },
                new()
                {//3
                    Id = 35,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_tim.webp",
                },
                new()
                {//4
                    Id = 36,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_vang.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(s24u);
        }



        private void AddDataToPhoneOptionTable(ModelBuilder modelBuilder)
        {
            var iPhone15ProMax256Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 1,
                    PhoneId = 1,
                    BuiltInStorageId = 2,
                    PhoneColorId = 1,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 2,
                    PhoneId = 1,
                    BuiltInStorageId = 2,
                    PhoneColorId = 2,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 3,
                    PhoneId = 1,
                    BuiltInStorageId = 2,
                    PhoneColorId = 3,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 4,
                    PhoneId = 1,
                    BuiltInStorageId = 2,
                    PhoneColorId = 4,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone15ProMax256Option);
            var iPhone15ProMax512Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 5,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 5,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 6,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 6,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 7,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 7,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 8,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 8,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone15ProMax512Option);
            var iPhone15Pro128Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 9,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 9,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 10,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 10,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 11,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 11,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 12,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 12,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone15Pro128Option);
            var iPhone15Pro256Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 13,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 13,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 14,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 14,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 15,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 15,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 16,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 16,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone15Pro256Option);
            var iPhone14ProMax256Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 17,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 21,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 18,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 22,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 19,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 23,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 20,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 24,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone14ProMax256Option);
            var iPhone14ProMax128Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 21,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 21,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 22,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 22,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 23,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 23,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 24,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 24,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone14ProMax128Option);
            var iPhone14Pro256Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 25,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 21,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 26,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 22,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 27,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 23,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 28,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 24,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone14Pro256Option);
            var iPhone14Pro128Option = new List<PhoneOption>
            {
                new()
                {
                    Id = 29,
                    PhoneId = 4,
                    BuiltInStorageId = 1,
                    PhoneColorId = 21,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 30,
                    PhoneId = 4,
                    BuiltInStorageId = 1,
                    PhoneColorId = 22,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 31,
                    PhoneId = 4,
                    BuiltInStorageId = 1,
                    PhoneColorId = 23,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 32,
                    PhoneId = 4,
                    BuiltInStorageId = 1,
                    PhoneColorId = 24,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone14Pro128Option);
        }

        private void AddDataSamsungToPhoneOptionTable(ModelBuilder modelBuilder)
        {
            var GalaxyS24256 = new List<PhoneOption>
            {
                new()
                {
                    Id = 33,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 29,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 34,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 30,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 35,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 31,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 36,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 32,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(GalaxyS24256);
            var GalaxyS24128 = new List<PhoneOption>
            {
                new()
                {
                    Id = 37,
                    PhoneId = 5,
                    BuiltInStorageId = 1,
                    PhoneColorId = 29,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 38,
                    PhoneId = 5,
                    BuiltInStorageId = 1,
                    PhoneColorId = 28,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 39,
                    PhoneId = 5,
                    BuiltInStorageId = 1,
                    PhoneColorId = 27,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 40,
                    PhoneId = 5,
                    BuiltInStorageId = 1,
                    PhoneColorId = 26,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(GalaxyS24128);
            var GalaxyS24Ultra = new List<PhoneOption>
            {
                new()
                {
                    Id = 41,
                    PhoneId = 6,
                    BuiltInStorageId = 1,
                    PhoneColorId = 33,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 42,
                    PhoneId = 6,
                    BuiltInStorageId = 1,
                    PhoneColorId = 34,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 43,
                    PhoneId = 6,
                    BuiltInStorageId = 1,
                    PhoneColorId = 35,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 44,
                    PhoneId = 6,
                    BuiltInStorageId = 1,
                    PhoneColorId = 36,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(GalaxyS24Ultra);
            var GalaxyS24Plus = new List<PhoneOption>
            {
                new()
                {
                    Id = 45,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 33,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 46,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 34,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 47,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 35,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 48,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 36,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(GalaxyS24Plus);

        }

        private void AddDataToPaymentTable(ModelBuilder modelBuilder)
        {
            var payments = new List<Payment>
            {
                new()
                {
                    Id = 1,
                    Name = "Thanh toán khi nhận hàng"
                },
                new()
                {
                    Id = 2,
                    Name = "MOMO"
                }
            };
            modelBuilder.Entity<Payment>().HasData(payments);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Phone>()
                .HasIndex(p => new { p.BrandId, p.Name })
                .IsUnique();

            modelBuilder.Entity<PhoneOption>()
                .HasKey(po => new { po.Id });

            modelBuilder.Entity<PhoneOption>()
                .HasIndex(po => new { po.PhoneId, po.BuiltInStorageId, po.PhoneColorId })
                .IsUnique();

            modelBuilder.Entity<PhoneOption>()
                .HasOne(po => po.Phone)
                .WithMany(po => po.PhoneOptions)
                .HasForeignKey(po => po.PhoneId);

            modelBuilder.Entity<PhoneOption>()
                .HasOne(po => po.BuiltInStorage)
                .WithMany(po => po.PhoneOptions)
                .HasForeignKey(po => po.BuiltInStorageId);

            modelBuilder.Entity<PhoneOption>()
                .HasOne(po => po.PhoneColor)
                .WithMany(po => po.PhoneOptions)
                .HasForeignKey(po => po.PhoneColorId);

            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.PhoneOptionId, ci.CartId });

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(ci => new { ci.PhoneOptionId, ci.OrderId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(ci => ci.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(ci => ci.OrderId);

            AddDataToRoleTable(modelBuilder);
            AddDataToBrandTable(modelBuilder);
            AddDataToPhoneTable(modelBuilder);
            AddDataToPhoneDetailTable(modelBuilder);
            AddDataToBuiltInStorageTable(modelBuilder);
            AddDataToPhoneColorTable(modelBuilder);
            AddDataToPhoneOptionTable(modelBuilder);
            AddDataSamsungToPhoneOptionTable(modelBuilder);
            AddDataToPaymentTable(modelBuilder);
        }
    }
}
