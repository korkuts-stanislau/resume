using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FiniteElements.BLL.Painters.Surfaces
{
    public interface IPaintable2D
    {
        void Paint(Graphics g, Rectangle paintField);
    }
}
