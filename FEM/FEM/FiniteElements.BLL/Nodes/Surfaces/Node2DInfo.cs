using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.BLL.Nodes.Surfaces
{
    public class Node2DInfo
    {
        public NodeType NodeType { get; set; }
        public double ForceValue { get; set; }
        public double ForceAngle { get; set; }
    }
}
