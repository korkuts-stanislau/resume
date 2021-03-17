using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.HashTables
{
    public abstract class HashFunction
    {
        //Ключ хеш функции, который определяет, как необходимо преобразовать входное значение в хеш
        public long Key { get; set; }
        public HashFunction(long key)
        {
            Key = key;
        }
        //Функция, возвращающая хеш от строки
        public abstract int GetHashValue(string text);
    }
}
