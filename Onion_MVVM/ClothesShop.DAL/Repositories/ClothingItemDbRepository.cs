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
    public class ClothingItemDbRepository : IRepository<ClothingItem>
    {
        private readonly ClothesShopContext _context;
        public ClothingItemDbRepository(ClothesShopContext context)
        {
            _context = context;
        }
        public void Create(ClothingItem item)
        {
            _context.ClothingItems.Add(item);
        }

        public void Delete(int id)
        {
            var item = _context.ClothingItems.Find(id);
            if(item != null)
            {
                _context.ClothingItems.Remove(item);
            }
            else
            {
                throw new Exception("Такая одежда не найдена");
            }
        }

        public IEnumerable<ClothingItem> Find(Func<ClothingItem, bool> predicate)
        {
            return _context.ClothingItems.Where(predicate).ToList();
        }

        public IEnumerable<ClothingItem> Get()
        {
            return _context.ClothingItems.ToList();
        }

        public ClothingItem Get(int id)
        {
            var item = _context.ClothingItems.Find(id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Такая одежда не найдена");
            }
        }

        public void Update(ClothingItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
