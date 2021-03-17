using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FiniteElements.BLL.Painters.Surfaces
{
    public class Nodes2DConverter
    {
        public Nodes2DConverter(Pen defaultPen, Brush defaultBrush)
        {
            Pen = defaultPen;
            Brush = defaultBrush;
        }
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public double XShift { get; set; }
        public double YShift { get; set; }
        public double MinXValue { get; set; }
        public double MinYValue { get; set; }
        public double MaxXValue { get; set; }
        public double MaxYValue { get; set; }
        public double YCenter { get; set; }
        public bool XCenterFlip { get; set; }
        public double XScaleCoefficient { get; set; }
        public double YScaleCoefficient { get; set; }

        public Node2D ConvertPrintNodeToNode(Node2D nodeForPrinting)
        {
            //Делаю копии узлов для дальнейшеего вывода. Копии могут изменяться для красивого отображения
            Node2D node = (Node2D)nodeForPrinting.Copy();

            //Отнимаю пользовательское смещение фигуры
            node.X -= XShift;
            node.Y -= YShift;

            // Масштабирую фигуру посредством уменьшения размера элементов
            node.X /= (double)XScaleCoefficient;
            node.Y /= (double)YScaleCoefficient;

            //Смещаю элемент сетки так, что бы левый верхний угол этой сетки находился в начале координат
            double yShift = MinYValue;
            double xShift = MinXValue;

            if (XCenterFlip)
            {
                //Если фигура перевернута, то надо пересчитать смещение по оси Y
                double minYValue = MaxYValue;
                minYValue -= YCenter;
                minYValue = -minYValue;
                minYValue += YCenter;
                yShift = minYValue;
            }
            node.X += xShift;
            node.Y += yShift;

            //Переворачиваю элемент сетки относительно её центра по оси X
            if (XCenterFlip)
            {
                node.Y -= YCenter;
                node.Y = -node.Y;
                node.Y += YCenter;
            }

            return node;
        }

        public Node2D ConvertNodeToPrintNode(Node2D node)
        {
            //Делаю копии узлов для дальнейшеего вывода. Копии могут изменяться для красивого отображения
            Node2D nodeForPrinting = (Node2D)node.Copy();

            //Переворачиваю элемент сетки относительно её центра по оси X
            if (XCenterFlip)
            {
                nodeForPrinting.Y -= YCenter;
                nodeForPrinting.Y = -nodeForPrinting.Y;
                nodeForPrinting.Y += YCenter;
            }

            //Смещаю элемент сетки так, что бы левый верхний угол этой сетки находился в начале координат
            double yShift = -MinYValue;
            double xShift = -MinXValue;

            if (XCenterFlip)
            {
                //Если фигура перевернута, то надо пересчитать смещение по оси Y
                double minYValue = MaxYValue;
                minYValue -= YCenter;
                minYValue = -minYValue;
                minYValue += YCenter;
                yShift = -minYValue;
            }
            nodeForPrinting.X += xShift;
            nodeForPrinting.Y += yShift;

            //Масштабирую фигуру посредством увеличения размера элементов
            nodeForPrinting.X *= XScaleCoefficient;
            nodeForPrinting.Y *= YScaleCoefficient;

            //Добавляю пользовательское смещение фигуры
            nodeForPrinting.X += XShift;
            nodeForPrinting.Y += YShift;

            return nodeForPrinting;
        }
    }
}
