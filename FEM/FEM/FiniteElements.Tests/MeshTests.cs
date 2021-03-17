using FiniteElements.BLL.Meshes;
using FiniteElements.BLL.Meshes.Surfaces;
using FiniteElements.BLL.Parsers;
using FiniteElements.BLL.Parsers.Surfaces;
using FiniteElements.BLL.SolutionTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.Tests
{
    class MeshTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StiffnessMatrixSizeTest()
        {
            Mesh2DLoader parser = new Mesh2DLoader(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            Mesh2D mesh = parser.ParseMeshFromCsv("nodes2D.csv", "triangleElements.csv", ';');
            ElasticitySolutionInfoParser parser2 = new ElasticitySolutionInfoParser(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            ElasticitySolutionInfo info = parser2.ParseTxt("elasticityInfo.txt");
            double[,] stiffness = mesh.GetStiffnessMatrix(info);
            Assert.AreEqual(10, stiffness.GetLength(0));
            Assert.AreEqual(10, stiffness.GetLength(1));
        }
    }
}
