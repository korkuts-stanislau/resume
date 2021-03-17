using FiniteElements.BLL.Interfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Painters;
using FiniteElements.BLL.SolutionTools;
using System.Collections.Generic;
using System.Drawing;

namespace FiniteElements.BLL.Elements
{
    public abstract class FiniteElement : IColored, ICopyable<FiniteElement>
    {
        public int Id { get; set; }
        public Brush Brush { get; set; }
        public List<Node> Nodes { get; set; }
        public abstract double Volume { get; }
        public FiniteElement(int id, Brush brush, List<Node> nodes)
        {
            Id = id;
            Brush = brush;
            Nodes = nodes;
        }
        public abstract double[,] GetStiffnessMatrix(ElasticitySolutionInfo info);
        public abstract double GetStress(double[] nodesMoves, ElasticitySolutionInfo info);
        public abstract double GetDisplacement(double[] nodesMoves, ElasticitySolutionInfo info);
        public abstract double GetDeformationX(double[] nodesMoves, ElasticitySolutionInfo info);
        public abstract double GetDeformationY(double[] nodesMoves, ElasticitySolutionInfo info);

        public abstract FiniteElement Copy();
    }
}
