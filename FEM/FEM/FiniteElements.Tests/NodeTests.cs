using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.Tests
{
    class NodeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ForceDecomposition()
        {
            Node2D node = new Node2D(-1, 1, 1);
            node.Type = NodeType.Force;
            node.ForceValue = 100;
            node.ForceAngle = 45 * Math.PI / 180;
            Assert.AreEqual(70.71, node.XForce, 2);
            Assert.AreEqual(70.71, node.YForce, 2);

            node.ForceAngle = 135 * Math.PI / 180;
            Assert.AreEqual(-70.71, node.XForce, 2);
            Assert.AreEqual(70.71, node.YForce, 2);

            node.ForceAngle = 225 * Math.PI / 180;
            Assert.AreEqual(-70.71, node.XForce, 2);
            Assert.AreEqual(-70.71, node.YForce, 2);

            node.ForceAngle = 315 * Math.PI / 180;
            Assert.AreEqual(70.71, node.XForce, 2);
            Assert.AreEqual(-70.71, node.YForce, 2);


            node.ForceAngle = 0 * Math.PI / 180;
            Assert.AreEqual(100, node.XForce, 2);
            Assert.AreEqual(0, node.YForce, 2);

            node.ForceAngle = 90 * Math.PI / 180;
            Assert.AreEqual(0, node.XForce, 2);
            Assert.AreEqual(100, node.YForce, 2);

            node.ForceAngle = 180 * Math.PI / 180;
            Assert.AreEqual(-100, node.XForce, 2);
            Assert.AreEqual(0, node.YForce, 2);

            node.ForceAngle = 270 * Math.PI / 180;
            Assert.AreEqual(0, node.XForce, 2);
            Assert.AreEqual(-100, node.YForce, 2);
        }

        [Test] 
        public void ForceSum()
        {
            Node2D node = new Node2D(-1, 1, 1);
            node.Type = NodeType.Force;
            node.ForceValue = 100;

            for(int i = 0; i < 360; i++)
            {
                node.ForceAngle = i * Math.PI / 180;
                Assert.AreEqual(100, Math.Sqrt(Math.Pow(node.XForce, 2) + Math.Pow(node.YForce, 2)), 2);
            }
        }
    }
}
