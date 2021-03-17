using FiniteElements.BLL.Meshes;
using FiniteElements.BLL.Meshes.Surfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.Parsers;
using FiniteElements.BLL.Parsers.Surfaces;
using FiniteElements.BLL.SolutionTools;
using FiniteElements.BLL.Solvers;
using FiniteElements.BLL.Solvers.Surfaces;
using NUnit.Framework;
using System;

namespace FiniteElements.Tests
{
    class SolverTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TriangleMeshSolverTest()
        {
            Elasticity2DSolver solver = new Elasticity2DSolver();
            Mesh2DLoader parser = new Mesh2DLoader(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            Mesh2D mesh = parser.ParseMeshFromCsv("nodes2D.csv", "triangleElements.csv", ';');
            mesh.Nodes[0].Type = NodeType.Fixed;
            mesh.Nodes[1].Type = NodeType.Fixed;
            mesh.Nodes[3].Type = NodeType.Force;
            ((Node2D)mesh.Nodes[3]).ForceValue = 1;
            ((Node2D)mesh.Nodes[3]).ForceAngle = Math.PI / 2;
            ElasticitySolutionInfoParser parser2 = new ElasticitySolutionInfoParser(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            ElasticitySolutionInfo info = parser2.ParseTxt("elasticityInfo.txt");
            double[] solution = solver.Solve(mesh, info);
            Assert.Pass();
        }
    }
}
