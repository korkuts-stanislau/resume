using NUnit.Framework;
using BinaryTrees;
using System;
using System.Collections.Generic;

namespace BinaryTreesTests
{
    public class NodeTreeTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Adding()
        {
            IntWrapper[] values = new IntWrapper[]
            {
                new IntWrapper(50), new IntWrapper(40), new IntWrapper(60), new IntWrapper(70), new IntWrapper(55), new IntWrapper(52),
                new IntWrapper(45), new IntWrapper(20)
            };
            BinaryTree<IntWrapper> tree = new NodeTree<IntWrapper>();
            tree.Add(values);
            tree.Add(new IntWrapper(119));
            Assert.AreEqual(values.Length + 1, tree.Count);
            foreach (var value in values)
            {
                Assert.IsTrue(tree.Contains(value));
            }
        }

        [Test]
        public void Removing()
        {
            BinaryTree<IntWrapper> tree = new NodeTree<IntWrapper>();
            tree.Add(new IntWrapper(61));
            IntWrapper[] values = new IntWrapper[]
            {
                new IntWrapper(40), new IntWrapper(60), new IntWrapper(70), new IntWrapper(55), new IntWrapper(52),
                new IntWrapper(45), new IntWrapper(20), new IntWrapper(65), new IntWrapper(80)
            };
            tree.Add(values);
            foreach (var value in values)
            {
                Assert.IsTrue(tree.Contains(value));
                tree.Remove(value);
                Assert.IsFalse(tree.Contains(value));
            }
        }

        [Test]
        public void RemovingWithRandomValues()
        {
            int valuesNumber = 1000;
            Random rand = new Random();
            List<IntWrapper> ints = new List<IntWrapper>();
            for (int i = 0; i < valuesNumber; i++)
            {
                ints.Add(new IntWrapper(rand.Next(-100000, 100000)));
            }
            var tree = new NodeTree<IntWrapper>();
            tree.Add(ints.ToArray());
            int count = 0;
            foreach (var value in ints)
            {
                Assert.IsTrue(tree.Contains(value));
                tree.Remove(value);
                count++;
                Assert.AreEqual(tree.Count, valuesNumber - count);
            }
        }

        [Test]
        public void Copy()
        {
            IntWrapper[] values = new IntWrapper[]
            {
                new IntWrapper(50), new IntWrapper(40), new IntWrapper(60), new IntWrapper(70), new IntWrapper(55), new IntWrapper(52),
                new IntWrapper(45), new IntWrapper(20)
            };
            BinaryTree<IntWrapper> tree = new NodeTree<IntWrapper>();
            tree.Add(values);
            IntWrapper[] valuesCopy = new IntWrapper[values.Length];
            tree.CopyTo(valuesCopy, 0);
            Array.Sort(values);
            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], valuesCopy[i]);
            }
        }

        [Test]
        public void LeavesCount()
        {
            IntWrapper[] values = new IntWrapper[]
            {
                new IntWrapper(50), new IntWrapper(30), new IntWrapper(70), new IntWrapper(20), new IntWrapper(40)
            };
            BinaryTree<IntWrapper> tree = new NodeTree<IntWrapper>();
            tree.Add(values);
            Assert.AreEqual(0, tree.GetLayerLeaves(0));
            Assert.AreEqual(1, tree.GetLayerLeaves(1));
            Assert.AreEqual(2, tree.GetLayerLeaves(2));
        }

        [Test]
        public void Rotate()
        {
            IntWrapper[] values = new IntWrapper[]
            {
                 new IntWrapper(1), new IntWrapper(50), new IntWrapper(30), new IntWrapper(70), new IntWrapper(20), new IntWrapper(40)
            };
            BinaryTree<IntWrapper> tree = new NodeTree<IntWrapper>();
            tree.Add(values);
            string listRepr = tree.GetTextForPrinting(PrintFormat.AsList);
            string tableRepr = tree.GetTextForPrinting(PrintFormat.AsTable);
            tree.RotateByNodeWithValue(new IntWrapper(50), Rotation.Right);
            Assert.AreEqual(listRepr, tree.GetTextForPrinting(PrintFormat.AsList));
            Assert.AreNotEqual(tableRepr, tree.GetTextForPrinting(PrintFormat.AsTable));
            tree.RotateByNodeWithValue(new IntWrapper(30), Rotation.Left);
            Assert.AreEqual(listRepr, tree.GetTextForPrinting(PrintFormat.AsList));
            Assert.AreEqual(tableRepr, tree.GetTextForPrinting(PrintFormat.AsTable));
        }

        [Test]
        public void RotateWithRandomValues()
        {
            int valuesNumber = 1000;
            Random rand = new Random();
            List<IntWrapper> ints = new List<IntWrapper>();
            for (int i = 0; i < valuesNumber; i++)
            {
                ints.Add(new IntWrapper(rand.Next(-100000, 100000)));
            }
            var tree = new NodeTree<IntWrapper>();
            tree.Add(ints.ToArray());
            string listRepr = tree.GetTextForPrinting(PrintFormat.AsList);
            string tableRepr = tree.GetTextForPrinting(PrintFormat.AsTable);
            tree.RotateByNodeWithValue(ints[1], Rotation.Right);
            Assert.AreEqual(listRepr, tree.GetTextForPrinting(PrintFormat.AsList));
            Assert.AreNotEqual(tableRepr, tree.GetTextForPrinting(PrintFormat.AsTable));
            listRepr = tree.GetTextForPrinting(PrintFormat.AsList);
            tableRepr = tree.GetTextForPrinting(PrintFormat.AsTable);
            tree.RotateByNodeWithValue(ints[1], Rotation.Left);
            Assert.AreEqual(listRepr, tree.GetTextForPrinting(PrintFormat.AsList));
            Assert.AreNotEqual(tableRepr, tree.GetTextForPrinting(PrintFormat.AsTable));
        }

        [Test]
        public void GetAllOneChild()
        {
            BinaryTree<IntWrapper> tree = new NodeTree<IntWrapper>();
            tree.Add(new IntWrapper(61));
            IntWrapper[] values = new IntWrapper[]
            {
                new IntWrapper(40), new IntWrapper(60), new IntWrapper(70), new IntWrapper(55), new IntWrapper(52),
                new IntWrapper(45), new IntWrapper(20), new IntWrapper(65), new IntWrapper(80)
            };
            tree.Add(values);
            Assert.AreEqual(3, tree.GetAllOneChildNodes().Count);
        }
    }
}