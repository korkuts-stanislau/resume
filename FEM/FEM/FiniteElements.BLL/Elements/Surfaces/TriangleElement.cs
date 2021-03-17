using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Accord.Math;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.SolutionTools;

namespace FiniteElements.BLL.Elements.Surfaces
{
    public class TriangleElement : FiniteElement2D
    {
        public TriangleElement(int id, Brush brush, List<Node2D> nodes, double thickness)
            : base(id, brush, nodes, thickness)
        {
            if (nodes.Count != 3)
            {
                throw new Exception("У треугольного элемента должно быть 3 узла");
            }
        }

        public override FiniteElement Copy()
        {
            List<Node2D> copies = new List<Node2D>();
            foreach (var node in Nodes)
            {
                copies.Add((Node2D)node.Copy());
            }
            return new TriangleElement(Id, Brush, copies, Thickness);
        }
        public override double GetStress(double[] nodesMoves, ElasticitySolutionInfo info)
        {
            double[] currentElementNodesMoves = new double[6];
            int i = 0;
            foreach (var node in Nodes)
            {
                currentElementNodesMoves[i * 2] = nodesMoves[node.Id * 2];
                currentElementNodesMoves[i * 2 + 1] = nodesMoves[node.Id * 2 + 1];
                i++;
            }
            double[,] IC = CoordinateMatrix.Inverse();
            double[,] D = new double[,]
            {
                {1, info.Coefficient, 0 },
                {info.Coefficient, 1, 0 },
                {0, 0, (1 - info.Coefficient) / 2 }
            }.Multiply(info.Modulus / (1 - Math.Pow(info.Coefficient, 2)));

            double[,] B = new double[3, 6];
            for (i = 0; i < 3; i++)
            {
                B[0, 2 * i + 0] = IC[1, i];
                B[0, 2 * i + 1] = 0.0;
                B[1, 2 * i + 0] = 0.0;
                B[1, 2 * i + 1] = IC[2, i];
                B[2, 2 * i + 0] = IC[2, i];
                B[2, 2 * i + 1] = IC[1, i];
            }
            var stress = D.Dot(B).Dot(currentElementNodesMoves);
            return Math.Sqrt(Math.Pow(stress[0], 2) + Math.Pow(stress[1], 2) - stress[0] * stress[1] + 3 * Math.Pow(stress[2], 2));
        }

        public override double GetDisplacement(double[] nodesMoves, ElasticitySolutionInfo info)
        {
            double[] currentElementNodesMoves = new double[6];
            int i = 0;
            foreach(var node in Nodes)
            {
                currentElementNodesMoves[i * 2] = nodesMoves[node.Id * 2];
                currentElementNodesMoves[i * 2 + 1] = nodesMoves[node.Id * 2 + 1];
                i++;
            }
            double displacement = 0;
            for(i = 0; i < 3; i++)
            {
                displacement += Math.Sqrt(Math.Pow(currentElementNodesMoves[i * 2], 2) + Math.Pow(currentElementNodesMoves[i * 2 + 1], 2));
            }
            return displacement;
        }

        public override double GetDeformationX(double[] nodesMoves, ElasticitySolutionInfo info)
        {

            double[] currentElementNodesMoves = new double[6];
            int i = 0;
            foreach (var node in Nodes)
            {
                currentElementNodesMoves[i * 2] = nodesMoves[node.Id * 2];
                currentElementNodesMoves[i * 2 + 1] = nodesMoves[node.Id * 2 + 1];
                i++;
            }
            double maxXDisplacement = double.MinValue;
            double minXDisplacement = double.MaxValue;
            for(i = 0; i < 3; i++)
            {
                if (currentElementNodesMoves[i * 2] > maxXDisplacement) maxXDisplacement = currentElementNodesMoves[i * 2];
                if (currentElementNodesMoves[i * 2] < minXDisplacement) minXDisplacement = currentElementNodesMoves[i * 2];
            }
            return Math.Abs(maxXDisplacement - minXDisplacement);
        }

        public override double GetDeformationY(double[] nodesMoves, ElasticitySolutionInfo info)
        {

            double[] currentElementNodesMoves = new double[6];
            int i = 0;
            foreach (var node in Nodes)
            {
                currentElementNodesMoves[i * 2] = nodesMoves[node.Id * 2];
                currentElementNodesMoves[i * 2 + 1] = nodesMoves[node.Id * 2 + 1];
                i++;
            }
            double maxYDisplacement = double.MinValue;
            double minYDisplacement = double.MaxValue;
            for (i = 0; i < 3; i++)
            {
                if (currentElementNodesMoves[i * 2 + 1] > maxYDisplacement) maxYDisplacement = currentElementNodesMoves[i * 2 + 1];
                if (currentElementNodesMoves[i * 2 + 1] < minYDisplacement) minYDisplacement = currentElementNodesMoves[i * 2 + 1];
            }
            return Math.Abs(maxYDisplacement - minYDisplacement);
        }

        public override double Volume
        {
            get => CoordinateMatrix.Determinant() / 2 * Thickness;
        }

        public override double[,] CoordinateMatrix
        {
            get => new double[,]
                {
                    {1, ((Node2D)Nodes[0]).X, ((Node2D)Nodes[0]).Y },
                    {1, ((Node2D)Nodes[1]).X, ((Node2D)Nodes[1]).Y },
                    {1, ((Node2D)Nodes[2]).X, ((Node2D)Nodes[2]).Y }
                };
        }

        public override double[,] GetStiffnessMatrix(ElasticitySolutionInfo info)
        {
            double[,] IC = CoordinateMatrix.Inverse();
            double[,] D = new double[,]
            {
                {1, info.Coefficient, 0 },
                {info.Coefficient, 1, 0 },
                {0, 0, (1 - info.Coefficient) / 2 }
            }.Multiply(info.Modulus / (1 - Math.Pow(info.Coefficient, 2)));

            double[,] B = new double[3, 6];
            for (int i = 0; i < 3; i++)
            {
                B[0, 2 * i + 0] = IC[1, i];
                B[0, 2 * i + 1] = 0.0;
                B[1, 2 * i + 0] = 0.0;
                B[1, 2 * i + 1] = IC[2, i];
                B[2, 2 * i + 0] = IC[2, i];
                B[2, 2 * i + 1] = IC[1, i];
            }

            return B.Transpose().Dot(D).Dot(B).Multiply(Volume);
        }
    }
}