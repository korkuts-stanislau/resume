using FiniteElements.BLL.Meshes;
using FiniteElements.BLL.Meshes.Surfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiniteElements.BLL.Solvers.Surfaces
{
    public class Elasticity2DSolver
    {
        //Вычисляет вектор узловых перемещений системы в виде сетки
        public double[] Solve(Mesh2D mesh, SolutionTools.ElasticitySolutionInfo info)
        {
            double[,] globalStiffnessMatrix = mesh.GetStiffnessMatrix(info);

            var nodes = mesh.Nodes.Select(node => (Node2D)node).ToList();

            ApplyConstraints(globalStiffnessMatrix, nodes);

            double[] nodeForces = GetNodeForcesVector(nodes);

            return Accord.Math.Matrix.Solve(globalStiffnessMatrix, nodeForces);
        }

        private double[] GetNodeForcesVector(List<Node2D> nodes)
        {
            var nodesCount = nodes.Count;
            double[] nodeForces = new double[nodesCount * 2];
            foreach(var node in nodes)
            {
                nodeForces[node.Id * 2] = node.XForce;
                nodeForces[node.Id * 2 + 1] = node.YForce;
            }
            return nodeForces;
        }
        private void ApplyConstraints(double[,] globalStiffnessMatrix, List<Node2D> nodes)
        {
            var nodesCount = nodes.Count;
            foreach(var node in nodes)
            {
                if(node.Type == NodeType.Fixed)
                {
                    for (int j = 0; j < nodesCount * 2; j++)
                    {
                        globalStiffnessMatrix[2 * node.Id, j] = 0;
                        globalStiffnessMatrix[2 * node.Id + 1, j] = 0;
                        globalStiffnessMatrix[j, 2 * node.Id] = 0;
                        globalStiffnessMatrix[j, 2 * node.Id + 1] = 0;
                    }
                    globalStiffnessMatrix[2 * node.Id, 2 * node.Id] = 1;
                    globalStiffnessMatrix[2 * node.Id + 1, 2 * node.Id + 1] = 1;
                }
            }
        }
    }
}
