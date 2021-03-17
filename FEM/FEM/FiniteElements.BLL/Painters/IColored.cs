using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FiniteElements.BLL.Painters
{
    public interface IColored
    {
        Brush Brush { get; }
    }
}
