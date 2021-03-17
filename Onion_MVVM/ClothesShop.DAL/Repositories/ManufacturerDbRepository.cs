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
    public class ManufacturerDbRepository : IRepository<Manufacturer>
    {
        private readonly ClothesShopContext _context;
        public ManufacturerDbRepository(ClothesShopContext context)
        {
            _context = context;
        }
        public void Create(Manufacturer item)
        {
            _context.Manufacturers.Add(item);
        }

        public void Delete(int id)
        {
            var man = _context.Manufacturers.Find(id);
            if (man != null)
            {
                _context.Manufacturers.Remove(man);
            }
            else
            {
                throw new Exception("Такой производитель не найден");
            }
        }

        public IEnumerable<Manufacturer> Find(Func<Manufacturer, bool> predicate)
        {
            return _context.Manufacturers.Where(predicate).ToList();
        }

        public IEnumerable<Manufacturer> Get()
        {
            return _context.Manufacturers.ToList();
        }

        public Manufacturer Get(int id)
        {
            var man = _context.Manufacturers.Find(id);
            if (man != null)
            {
                return man;
            }
            else
            {
                throw new Exception("Такой производитель не найден");
            }
        }

        public void Update(Manufacturer item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
