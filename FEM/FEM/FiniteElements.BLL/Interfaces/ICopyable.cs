using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.BLL.Interfaces
{
    interface ICopyable<T>
    {
        T Copy();
    }
}
