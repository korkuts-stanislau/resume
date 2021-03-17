using ClothesShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesShop.DAL.EF
{
    public class ClothesShopContext : DbContext
    {
        public DbSet<ClothingItemType> ClothingItemTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ClothingItem> ClothingItems { get; set; }
        public ClothesShopContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
