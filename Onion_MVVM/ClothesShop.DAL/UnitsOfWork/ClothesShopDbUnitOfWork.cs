using ClothesShop.DAL.EF;
using ClothesShop.DAL.Entities;
using ClothesShop.DAL.Interfaces;
using ClothesShop.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesShop.DAL.UnitsOfWork
{
    public class ClothesShopDbUnitOfWork : IUnitOfWork
    {
        private readonly ClothesShopContext _context;
        private ClothingItemDbRepository _clothingItems;
        private ClothingItemTypeDbRepository _clothingItemTypes;
        private ManufacturerDbRepository _manufacturers;
        public ClothesShopDbUnitOfWork(string connection)
        {
            _context = new ClothesShopContext(new DbContextOptionsBuilder()
                .UseSqlServer(connection)
                .Options);
        }
        public IRepository<ClothingItemType> ClothingItemTypes
        {
            get
            {
                if(_clothingItemTypes == null)
                {
                    _clothingItemTypes = new ClothingItemTypeDbRepository(_context);
                }
                return _clothingItemTypes;
            }
        }
        public IRepository<Manufacturer> Manufacturers
        {
            get
            {
                if(_manufacturers == null)
                {
                    _manufacturers = new ManufacturerDbRepository(_context);
                }
                return _manufacturers;
            }
        }
        public IRepository<ClothingItem> ClothingItems
        {
            get
            {
                if(_clothingItems == null)
                {
                    _clothingItems = new ClothingItemDbRepository(_context);
                }
                return _clothingItems;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
