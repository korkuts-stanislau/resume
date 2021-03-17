using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.BLL.Painters
{
    public class GradientMaker
    {
        public byte[] GetRGBGradientValue(double minValue, double maxValue, double value)
        {
            double ratio = 2 * (value - minValue) / (maxValue - minValue);
            byte B = (byte)Math.Max(0, 255 * (1 - ratio));
            byte R = (byte)Math.Max(0, 255 * (ratio - 1));
            byte G = (byte)(255 - B - R);
            return new byte[] { R, G, B };
        }
    }
}
