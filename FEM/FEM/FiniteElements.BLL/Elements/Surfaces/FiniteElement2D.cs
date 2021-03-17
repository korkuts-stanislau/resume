using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.Painters;
using FiniteElements.BLL.Painters.Surfaces;
using FiniteElements.BLL.SolutionTools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FiniteElements.BLL.Elements.Surfaces
{
    public abstract class FiniteElement2D : FiniteElement
    {
        public FiniteElement2D(int id, Brush brush, List<Node2D> nodes, double thickness) : 
            base(id, brush, new List<Node>(nodes))
        {
            Thickness = thickness;
        }
        public double Thickness { get; }
        public abstract double[,] CoordinateMatrix { get; }
        public void Paint(Graphics g, Nodes2DConverter info)
        {
            //Делаю копии узлов для дальнейшеего вывода. Копии могут изменяться для красивого отображения
            Node2D[] nodesToPrint = new Node2D[Nodes.Count];
            for (int i = 0; i < Nodes.Count; i++)
            {
                nodesToPrint[i] = info.ConvertNodeToPrintNode((Node2D)Nodes[i]);
            }

            PointF[] points = new PointF[nodesToPrint.Length];
            for(int i = 0; i < nodesToPrint.Length; i++)
            {
                points[i] = new PointF((float)nodesToPrint[i].X, (float)nodesToPrint[i].Y);
            }

            g.FillPolygon(Brush, points);

            for (int i = 0; i < nodesToPrint.Length; i++)
            {

                if (i == nodesToPrint.Length - 1)
                {
                    g.DrawLine(new Pen(Brushes.Black), points[i], points[0]);
                }
                else
                {
                    g.DrawLine(new Pen(Brushes.Black), points[i], points[i + 1]);
                }
            }
        }
    }
}
