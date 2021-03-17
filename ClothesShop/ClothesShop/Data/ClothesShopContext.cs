using System;
using Microsoft.EntityFrameworkCore;
using ClothesShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Data
{
    public class ClothesShopContext : DbContext
    {
        public ClothesShopContext()
        {
            Database.EnsureCreated();
        }

        public ClothesShopContext(DbContextOptions<ClothesShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<ClothingItem> ClothingItems { get; set; }
        public DbSet<ClothingItemType> ClothingItemTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderClothingItem> OrderClothingItems { get; set; }
    }
}
