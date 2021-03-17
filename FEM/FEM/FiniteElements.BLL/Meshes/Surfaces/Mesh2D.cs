using FiniteElements.BLL.Elements;
using FiniteElements.BLL.Elements.Surfaces;
using FiniteElements.BLL.Interfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.Painters;
using FiniteElements.BLL.Painters.Surfaces;
using FiniteElements.BLL.SolutionTools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FiniteElements.BLL.Meshes.Surfaces
{
    public class Mesh2D : Mesh, IPaintable2D
    {
        public Mesh2D(List<FiniteElement2D> elements, List<Node2D> nodes) :
            base(new List<FiniteElement>(elements), new List<Node>(nodes))
        {

        }

        public override Mesh Copy()
        {
            List<FiniteElement2D> elementCopies = new List<FiniteElement2D>();
            List<Node2D> nodeCopies = new List<Node2D>();
            foreach(var el in Elements)
            {
                elementCopies.Add((FiniteElement2D)el.Copy());
            }
            foreach (var node in Nodes)
            {
                nodeCopies.Add((Node2D)node.Copy());
            }
            return new Mesh2D(elementCopies, nodeCopies);
        }

        public void ChangeNodeState(double x, double y, Rectangle paintField, Node2DInfo state)
        {
            try
            {
                Node2D node = FindNodeByPaintCoordinates(x, y, paintField);
                node.ChangeState(state);
            }
            catch (Exception exception)
            {
                //Ну не нашли, значит и не поменяли, всё логично
            }
        }

        public void PaintElementsByCharacteristics(double[] nodesMoves, ElasticitySolutionInfo info, MeshPaintCharacteristicType type)
        {
            double minCharacteristic = double.MaxValue;
            double maxCharacteristic = double.MinValue;
            foreach(var el in Elements)
            {
                double characteristic = 0;
                switch(type)
                {
                    case MeshPaintCharacteristicType.Displacements:
                        characteristic = ((FiniteElement2D)el).GetDisplacement(nodesMoves, info);
                        break;
                    case MeshPaintCharacteristicType.Stresses:
                        characteristic = ((FiniteElement2D)el).GetStress(nodesMoves, info);
                        break;
                    case MeshPaintCharacteristicType.XDeformations:
                        characteristic = ((FiniteElement2D)el).GetDeformationX(nodesMoves, info);
                        break;
                    case MeshPaintCharacteristicType.YDeformations:
                        characteristic = ((FiniteElement2D)el).GetDeformationY(nodesMoves, info);
                        break;
                }
                if (characteristic < minCharacteristic) minCharacteristic = characteristic;
                if (characteristic > maxCharacteristic) maxCharacteristic = characteristic;
            }

            GradientMaker grad = new GradientMaker();

            foreach (var el in Elements)
            {
                double characteristic = 0;
                switch (type)
                {
                    case MeshPaintCharacteristicType.Displacements:
                        characteristic = ((FiniteElement2D)el).GetDisplacement(nodesMoves, info);
                        break;
                    case MeshPaintCharacteristicType.Stresses:
                        characteristic = ((FiniteElement2D)el).GetStress(nodesMoves, info);
                        break;
                    case MeshPaintCharacteristicType.XDeformations:
                        characteristic = ((FiniteElement2D)el).GetDeformationX(nodesMoves, info);
                        break;
                    case MeshPaintCharacteristicType.YDeformations:
                        characteristic = ((FiniteElement2D)el).GetDeformationY(nodesMoves, info);
                        break;
                }
                byte[] RGB = grad.GetRGBGradientValue(minCharacteristic, maxCharacteristic, characteristic);
                el.Brush = new SolidBrush(Color.FromArgb(200, RGB[0], RGB[1], RGB[2]));
            }
        }

        public override double[,] GetStiffnessMatrix(ElasticitySolutionInfo info)
        {
            double[,] stiffnessMatrix = new double[Nodes.Count * 2, Nodes.Count * 2];
            foreach (var el in Elements)
            {
                int nodesCount = el.Nodes.Count;
                double[,] elStiffness = el.GetStiffnessMatrix(info);
                for (int i = 0; i < nodesCount; i++)
                {
                    for (int j = 0; j < nodesCount; j++)
                    {
                        stiffnessMatrix[2 * el.Nodes[i].Id, 2 * el.Nodes[j].Id] += elStiffness[2 * i, 2 * j];
                        stiffnessMatrix[2 * el.Nodes[i].Id, 2 * el.Nodes[j].Id + 1] += elStiffness[2 * i, 2 * j + 1];
                        stiffnessMatrix[2 * el.Nodes[i].Id + 1, 2 * el.Nodes[j].Id] += elStiffness[2 * i + 1, 2 * j];
                        stiffnessMatrix[2 * el.Nodes[i].Id + 1, 2 * el.Nodes[j].Id + 1] += elStiffness[2 * i + 1, 2 * j + 1];
                    }
                }
            }
            return stiffnessMatrix;
        }

        private Node2D FindNodeByPaintCoordinates(double x, double y, Rectangle paintField)
        {
            Nodes2DConverter info = FillPaintInfo(paintField);
            Node2D currentNode = new Node2D(-1, x, y);
            foreach (var node in Nodes)
            {
                var paintNode = info.ConvertNodeToPrintNode((Node2D)node);
                if(paintNode.GetDistanceTo(currentNode) < 10)
                {
                    return (Node2D)node;
                }
            }
            throw new Exception("По этим координатам узла нет");
        }

        public void Paint(Graphics g, Rectangle paintField)
        {
            Nodes2DConverter info = FillPaintInfo(paintField);
            foreach(var el in Elements)
            {
                try
                {
                    ((FiniteElement2D)el).Paint(g, info);
                }
                catch(Exception exception)
                {
                    throw new Exception($"Ошибка отрисовки. Не удалось отрисовать элемент с id = {el.Id}. {exception.Message}");
                }
            }
            foreach (var node in Nodes)
            {
                try
                {
                    ((Node2D)node).Paint(g, info);
                }
                catch (Exception exception)
                {
                    throw new Exception($"Ошибка отрисовки. Не удалось отрисовать узел с id = {node.Id}. {exception.Message}");
                }
            }
        }

        private Nodes2DConverter FillPaintInfo(Rectangle paintField)
        {
            Nodes2DConverter info = new Nodes2DConverter(new Pen(Brushes.Black), Brushes.Black);
            info.XShift = 25;
            info.YShift = 25;
            info.MinXValue = double.MaxValue;
            info.MinYValue = double.MaxValue;
            info.MaxXValue = double.MinValue;
            info.MaxYValue = double.MinValue;
            foreach(var el in Elements)
            {
                foreach(var node in el.Nodes)
                {
                    if(((Node2D)node).X < info.MinXValue)
                    {
                        info.MinXValue = ((Node2D)node).X;
                    }
                    if (((Node2D)node).X > info.MaxXValue)
                    {
                        info.MaxXValue = ((Node2D)node).X;
                    }
                    if (((Node2D)node).Y < info.MinYValue)
                    {
                        info.MinYValue = ((Node2D)node).Y;
                    }
                    if (((Node2D)node).Y > info.MaxYValue)
                    {
                        info.MaxYValue = ((Node2D)node).Y;
                    }
                }
            }
            info.YCenter = (info.MaxYValue + info.MinYValue) / 2;
            info.XCenterFlip = true;

            info.XScaleCoefficient = (paintField.Width - info.XShift * 2) / Math.Abs(info.MaxXValue - info.MinXValue);
            info.YScaleCoefficient = (paintField.Height - info.YShift * 2) / Math.Abs(info.MaxYValue - info.MinYValue);

            return info;
        }

        public void MoveMesh(double[] nodesMovingVector)
        {
            if (nodesMovingVector.Length != Nodes.Count * 2)
            {
                throw new Exception("Неверная размерность вектора узловых перемещений");
            }
            foreach (var node in Nodes)
            {
                ((Node2D)node).X += nodesMovingVector[node.Id * 2];
                ((Node2D)node).Y += nodesMovingVector[node.Id * 2 + 1];
            }
        }
    }
}
