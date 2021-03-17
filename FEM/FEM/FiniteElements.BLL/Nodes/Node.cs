using FiniteElements.BLL.Interfaces;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.Painters;
using System.Drawing;

namespace FiniteElements.BLL.Nodes
{
    public enum NodeType
    {
        Fixed,
        Force,
        None
    }
    public abstract class Node : IColored, ICopyable<Node>
    {
        public Node(int id, NodeType type = NodeType.None)
        {
            Id = id;
            Type = type;
        }
        public int Id { get; set; }
        public abstract Brush Brush { get; }
        public NodeType Type { get; set; }
        public abstract Node Copy();
    }
}
