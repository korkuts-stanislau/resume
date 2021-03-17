using Identifiers.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.HashTables
{
    /// <summary>
    /// Класс, реализующий хеш таблицу
    /// </summary>
    /// <typeparam name="T">Класс, содержащий поле Key, хеш которого можно получить</typeparam>
    public class HashTable<T> where T : class, IHashable, IKeyable
    {
        T[] _values; //Массив, где хранятся классы типа T хеш таблицы
        HashFunction _function; //Хеш функция, при помощи которой хеш таблица вычисляет индекс элемента в массиве
        public HashTable(int tableSize, HashFunction function)
        {
            _values = new T[tableSize];
            _function = function;
        }
        /// <summary>
        /// Метод добавления элемента в хеш таблицу
        /// </summary>
        /// <param name="value">Добавляемый объект</param>
        public void Add(T value)
        {
            int hash = value.GetHash(_function); //Получение хеша(места в массиве) от исходного объекта
            if(hash > _values.Length) //Если хеш выходит за пределы массива, расширяем массив
            {
                ExpandArray(hash + 1);
            }
            if(_values[hash] != null) //Если при добавлении мы замечаем что в этом месте уже есть, элемент
                //значит произошла коллизия. Она решается квадратичным методом
            {
                int i = 1;
                while(_values[hash] != null) //Пока мы не найдём такое значение хеша, где нет элемента
                    //При том что каждую итерацию мы переходим на следующий индекс квадратично
                    //Если получившийся хеш = 139, то мы будем проходить по значениям массива так
                    //139, 139 + 1^2, 139 + 2^2, 139 + 3^2 и так далее, пока не найдём пустое место в массиве
                {
                    hash += (int)Math.Pow(i, 2);
                    if(hash > _values.Length)
                    {
                        ExpandArray(hash + 1);
                    }
                    i++;
                }
            }
            _values[hash] = value;
        }

        private void ExpandArray(int length)
        {
            T[] newArr = new T[length];
            _values.CopyTo(newArr, 0);
            _values = newArr;
        }

        /// <summary>
        /// Метод, производящий поиск элемента в массиве по ключу
        /// </summary>
        /// <param name="key">Ключ элемента, которого необходимо найти</param>
        /// <returns>Найденный элемент</returns>
        public T Find(string key)
        {
            int hash = _function.GetHashValue(key); //Получение хеша от входного ключа
            if(hash > _values.Length) //Если хеш превысил размер массива, значит такой элемент не добавляли
            {
                throw new Exception("Идентификатор не найден");
            }
            int i = 1;
            while(_values[hash] != null && _values[hash].Key != key) //Проходимся по массиву, в том
                //же квадратичном виде как и в добавлении, пока значения в этих индексах не пустые и ключ элемента
                //не равен необходимому ключу
            {
                hash += (int)Math.Pow(i, 2);
                if (hash > _values.Length)
                {
                    throw new Exception("Идентификатор не найден");
                }
                i++;
            }
            //Если после прохода значение элемента пустое, то идентификатор не найден
            if(_values[hash] == null)
            {
                throw new Exception("Идентификатор не найден");
            }
            //Иначе это значение которое необходимо было найти, так как их улючи совпадают
            return _values[hash];
        }
    }
}
