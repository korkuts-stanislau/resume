using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.SolutionTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FiniteElements.BLL.Elements.Surfaces
{
    public class QuadriteralElement : FiniteElement2D
    {
        private TriangleElement[] _parts;
        private Dictionary<int, int> _partsMap;
        public QuadriteralElement(int id, Brush brush, List<Node2D> nodes, double thickness)
            : base(id, brush, nodes, thickness)
        {
            if (nodes.Count != 4)
            {
                throw new Exception("У четырёхугольного элемента должно быть 4 узла");
            }
            _parts = new TriangleElement[]
            {
                new TriangleElement(-1, brush, new List<Node2D>( 
                    new Node2D[] {(Node2D)Nodes[0].Copy(), (Node2D)Nodes[1].Copy(), (Node2D)Nodes[2].Copy()}), Thickness),
                new TriangleElement(-1, brush, new List<Node2D>(
                    new Node2D[] { (Node2D)Nodes[0].Copy(), (Node2D)Nodes[2].Copy(), (Node2D)Nodes[3].Copy()}), Thickness)
            };
            _partsMap = new Dictionary<int, int>();
            _partsMap.Add(Nodes[0].Id, 0);
            _partsMap.Add(Nodes[1].Id, 1);
            _partsMap.Add(Nodes[2].Id, 2);
            _partsMap.Add(Nodes[3].Id, 3);

        }
        public override double GetDisplacement(double[] nodesMoves, ElasticitySolutionInfo info)
        {
            return _parts[0].GetDisplacement(nodesMoves, info) + _parts[1].GetDisplacement(nodesMoves, info);
        }

        public override double GetStress(double[] nodesMoves, ElasticitySolutionInfo info)
        {
            return _parts[0].GetStress(nodesMoves, info) + _parts[1].GetStress(nodesMoves, info);
        }

        public override double GetDeformationX(double[] nodesMoves, ElasticitySolutionInfo info)
        {
            return _parts[0].GetDeformationX(nodesMoves, info) + _parts[1].GetDeformationX(nodesMoves, info);
        }

        public override double GetDeformationY(double[] nodesMoves, ElasticitySolutionInfo info)
        {
            return _parts[0].GetDeformationY(nodesMoves, info) + _parts[1].GetDeformationY(nodesMoves, info);
        }

        public override FiniteElement Copy()
        {
            List<Node2D> copies = new List<Node2D>();
            foreach(var node in Nodes)
            {
                copies.Add((Node2D)node.Copy());
            }
            return new QuadriteralElement(Id, Brush, copies, Thickness);
        }
        public override double Volume => _parts[0].Volume + _parts[1].Volume;

        public override double[,] CoordinateMatrix => throw new Exception("У этого элемента нет координатной матрицы");

        public override double[,] GetStiffnessMatrix(ElasticitySolutionInfo info)
        {
            //Надо поменять формирование матрицы жесткости
            double[,] stiffnessMatrix = new double[8, 8];
            foreach(var el in _parts)
            {
                double[,] elStiffness = el.GetStiffnessMatrix(info);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        stiffnessMatrix[2 * _partsMap[el.Nodes[i].Id], 2 * _partsMap[el.Nodes[j].Id]] += elStiffness[2 * i, 2 * j];
                        stiffnessMatrix[2 * _partsMap[el.Nodes[i].Id], 2 * _partsMap[el.Nodes[j].Id] + 1] += elStiffness[2 * i, 2 * j + 1];
                        stiffnessMatrix[2 * _partsMap[el.Nodes[i].Id] + 1, 2 * _partsMap[el.Nodes[j].Id]] += elStiffness[2 * i + 1, 2 * j];
                        stiffnessMatrix[2 * _partsMap[el.Nodes[i].Id] + 1, 2 * _partsMap[el.Nodes[j].Id] + 1] += elStiffness[2 * i + 1, 2 * j + 1];
                    }
                }
            }
            return stiffnessMatrix;
        }
    }
}
