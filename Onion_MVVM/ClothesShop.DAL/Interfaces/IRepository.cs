using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesShop.DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> Get();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
