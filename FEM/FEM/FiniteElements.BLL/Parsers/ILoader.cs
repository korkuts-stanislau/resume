using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.BLL.Parsers
{
    interface ILoader<T>
    {
        T LoadFromTxt(string pathToTxt);
        T LoadFromCsv(string pathToCsv, char separator);
        T LoadFromBd(string connection);
    }
}
