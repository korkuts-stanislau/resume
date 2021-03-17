using FiniteElements.BLL.Elements;
using FiniteElements.BLL.Elements.Surfaces;
using FiniteElements.BLL.Meshes;
using FiniteElements.BLL.Meshes.Surfaces;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.Parsers;
using FiniteElements.BLL.Parsers.Surfaces;
using FiniteElements.BLL.SolutionTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.Tests
{
    class FiniteElementTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TriangleElementStiffnessMatrixSizeTest()
        {
            Mesh2DLoader parser = new Mesh2DLoader(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            Mesh2D mesh = parser.ParseMeshFromCsv("nodes2D.csv", "triangleElements.csv", ';');
            ElasticitySolutionInfoParser parser2 = new ElasticitySolutionInfoParser(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            ElasticitySolutionInfo info = parser2.ParseTxt("elasticityInfo.txt");
            double[,] stiffness = mesh.Elements[0].GetStiffnessMatrix(info);
            Assert.AreEqual(6, stiffness.GetLength(0));
            Assert.AreEqual(6, stiffness.GetLength(1));
        }

        [Test]
        public void TriangleElementAreaTest()
        {
            FiniteElement2D el = new TriangleElement(-1, null, new List<Node2D>(new Node2D[]
            {
                new Node2D(-1, 0, 0),
                new Node2D(-1, 0, 1),
                new Node2D(-1, 1, 0)
            }), 0.001);
            Assert.AreEqual(0.0005, el.Volume, 4);
        }

        [Test]
        public void TriangleElementCoordinateMatrixTest()
        {
            FiniteElement2D el = new TriangleElement(-1, null, new List<Node2D>(new Node2D[]
            {
                new Node2D(-1, 0, 0),
                new Node2D(-1, 0, 1),
                new Node2D(-1, 1, 0)
            }), 0.001);
            double[,] array = new double[,]
            {
                {1, 0, 0},
                {1, 0, 1},
                {1, 1, 0}
            };
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(array[i, j], el.CoordinateMatrix[i, j]);
                }
            }
        }
    }
}
