using FiniteElements.BLL.Elements;
using FiniteElements.BLL.Interfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.SolutionTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteElements.BLL.Meshes
{
    public enum MeshPaintCharacteristicType
    {
        Displacements,
        Stresses,
        XDeformations,
        YDeformations
    }
    public abstract class Mesh : ICopyable<Mesh>
    {
        public List<FiniteElement> Elements { get; set; }
        public List<Node> Nodes { get; set; }

        public Mesh(List<FiniteElement> elements, List<Node> nodes)
        {
            Elements = elements;
            Nodes = nodes;
        }
        public abstract Mesh Copy();
        public abstract double[,] GetStiffnessMatrix(ElasticitySolutionInfo info);

        public void ClearNodesTypes()
        {
            foreach (var node in Nodes)
            {
                node.Type = NodeType.None;
            }
        }
    }
}
