using ClothesShop.DAL.EF;
using ClothesShop.DAL.Entities;
using ClothesShop.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClothesShop.DAL.Repositories
{
    public class ClothingItemTypeDbRepository : IRepository<ClothingItemType>
    {
        private readonly ClothesShopContext _context;
        public ClothingItemTypeDbRepository(ClothesShopContext context)
        {
            _context = context;
        }
        public void Create(ClothingItemType item)
        {
            _context.ClothingItemTypes.Add(item);
        }

        public void Delete(int id)
        {
            var type = _context.ClothingItemTypes.Find(id);
            if(type != null)
            {
                _context.ClothingItemTypes.Remove(type);
            }
            else
            {
                throw new Exception("Такой тип одежды не найден");
            }
        }

        public IEnumerable<ClothingItemType> Find(Func<ClothingItemType, bool> predicate)
        {
            return _context.ClothingItemTypes.Where(predicate).ToList();
        }

        public IEnumerable<ClothingItemType> Get()
        {
            return _context.ClothingItemTypes.ToList();
        }

        public ClothingItemType Get(int id)
        {
            var type = _context.ClothingItemTypes.Find(id);
            if (type != null)
            {
                return type;
            }
            else
            {
                throw new Exception("Такой тип одежды не найден");
            }
        }

        public void Update(ClothingItemType item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
