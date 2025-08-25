using System;
using System.Collections.Generic;

namespace KratosCore
{
    public interface IElement
    {
        int Id { get; }
        IGeometry Geometry { get; }
        IReadOnlyList<INode> Nodes { get; }
        
        void CalculateLocalSystem(out double[,] leftHandSideMatrix, out double[] rightHandSideVector);
        void CalculateMassMatrix(out double[,] massMatrix);
        void CalculateStiffnessMatrix(out double[,] stiffnessMatrix);
        
        IElement Clone(int newId, IReadOnlyList<INode> newNodes);
        void Initialize();
    }
}
