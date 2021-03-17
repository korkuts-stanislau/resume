using FiniteElements.BLL.Painters;
using FiniteElements.BLL.Painters.Surfaces;
using System;
using System.Drawing;

namespace FiniteElements.BLL.Nodes.Surfaces
{
    public class Node2D : Node
    {
        public Node2D(int id, double x, double y) : base(id)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double ForceValue { get; set; }
        /// <summary>
        /// Угол приложенной силы в радианах
        /// </summary>
        public double ForceAngle { get; set; }
        public override Brush Brush
        {
            get
            {
                switch(Type)
                {
                    case NodeType.Force:
                        return Brushes.Red;
                    case NodeType.Fixed:
                        return Brushes.Blue;
                    case NodeType.None:
                        return Brushes.Black;
                    default:
                        throw new Exception("Не определен цвет для такого класса узла");
                }
            }
        }
        public override Node Copy()
        {
            return new Node2D(Id, X, Y);
        }
        public double XForce
        {
            get
            {
                if (Type == NodeType.Force)
                {
                    return ForceValue * Math.Cos(ForceAngle);
                }
                return 0;
            }
        }
        public double YForce
        {
            get
            {
                if (Type == NodeType.Force)
                {
                    return ForceValue * Math.Sin(ForceAngle);
                }
                return 0;
            }
        }
        public void ChangeState(Node2DInfo state)
        {
            Type = state.NodeType;
            ForceValue = state.ForceValue;
            ForceAngle = state.ForceAngle;
        }
        public double GetDistanceTo(Node2D node)
        {
            return Math.Sqrt(Math.Pow(X - node.X, 2) + Math.Pow(Y - node.Y, 2));
        }

        public void Paint(Graphics g, Nodes2DConverter info)
        {
            float ellipseRadius = 3;
            Node2D nodeForPrinting = info.ConvertNodeToPrintNode(this);
            g.FillEllipse(Brush, new RectangleF((float)nodeForPrinting.X - ellipseRadius, (float)nodeForPrinting.Y - ellipseRadius,
                ellipseRadius * 2, ellipseRadius * 2));
            g.DrawString($"{(int)(X * 1000)}, {(int)(Y * 1000)}\nmm", new Font(FontFamily.GenericSerif, 6, FontStyle.Regular), Brush, (float)nodeForPrinting.X, (float)nodeForPrinting.Y);
            if(Type == NodeType.Force)
            {
                PointF currentPoint = new PointF((float)nodeForPrinting.X, (float)nodeForPrinting.Y);
                PointF forcePoint = new PointF((float)(nodeForPrinting.X + Math.Cos(-ForceAngle) * ForceValue), 
                    (float)(nodeForPrinting.Y + Math.Sin(-ForceAngle) * ForceValue));
                g.DrawLine(new Pen(Brush), currentPoint, forcePoint);
            }
        }
    }
}
