using ClothesShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesShop.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<ClothingItemType> ClothingItemTypes { get; }
        public IRepository<Manufacturer> Manufacturers { get; }
        public IRepository<ClothingItem> ClothingItems { get; }
        void Save();
    }
}
