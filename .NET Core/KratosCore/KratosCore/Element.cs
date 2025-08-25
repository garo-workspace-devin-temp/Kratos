using System;
using System.Collections.Generic;

namespace KratosCore
{
    public abstract class Element : IElement
    {
        public int Id { get; }
        public IGeometry Geometry { get; protected set; }
        public IReadOnlyList<INode> Nodes { get; protected set; }

        protected Element(int id, IGeometry geometry, IReadOnlyList<INode> nodes)
        {
            Id = id;
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
        }

        public abstract void CalculateLocalSystem(out double[,] leftHandSideMatrix, out double[] rightHandSideVector);
        public abstract void CalculateMassMatrix(out double[,] massMatrix);
        public abstract void CalculateStiffnessMatrix(out double[,] stiffnessMatrix);
        public abstract IElement Clone(int newId, IReadOnlyList<INode> newNodes);

        public virtual void Initialize()
        {
        }

        protected int GetDegreesOfFreedom()
        {
            return Nodes.Count * 3;
        }

        public override string ToString()
        {
            return $"Element {Id} with {Nodes.Count} nodes";
        }
    }
}
