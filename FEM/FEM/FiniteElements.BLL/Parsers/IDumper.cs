using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.BLL.Parsers
{
    interface IDumper<T>
    {
        void DumpToTxt(string pathToTxt, T item);
        void DumpToCsv(string pathToCsv, T item);
        void DumpToBd(string connection, T item);
    }
}
