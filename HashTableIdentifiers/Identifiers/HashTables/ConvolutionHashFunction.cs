using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.HashTables
{
    public class ConvolutionHashFunction : HashFunction
    {
        public ConvolutionHashFunction(long key) : base(key)
        {

        }
        /// <summary>
        /// Функция, возвращающая хеш в диапазоне 0, 10006
        /// </summary>
        /// <param name="text">Текст, хеш которого надо вернуть</param>
        /// <returns>Хеш по входному тексту</returns>
        public override int GetHashValue(string text)
        {
            if(text == "")
            {
                throw new Exception("Пустая строка не может быть ключом");
            }
            int hash = 0;
            //Выбираю 5 цифр из ключа, индекс начала которых это кодовое представление первого символа строки text деленное по модулю 8
            int key = int.Parse(Key.ToString().Substring(text[0] % 8, 5));
            //Аккумулирую хеш сумму
            foreach(char symb in text)
            {
                hash += key + symb;
            }
            //Для того что бы не возвращать огромное значение, возвращаю хеш по модулю 10007
            //Итоговый результат получается в диапазоне от 0 до 10006
            return hash % 10007;
        }
    }
}
