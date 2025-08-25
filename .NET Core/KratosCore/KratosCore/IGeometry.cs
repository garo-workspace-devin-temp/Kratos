using System;
using System.Collections.Generic;

namespace KratosCore
{
    public interface IGeometry
    {
        int Id { get; }
        string GeometryType { get; }
        int PointsCount { get; }
        IReadOnlyList<INode> Points { get; }
        
        double Area();
        double Volume();
        void GetShapeFunctions(double[] localCoordinates, out double[] shapeFunctions);
        void GetShapeFunctionDerivatives(double[] localCoordinates, out double[,] derivatives);
    }
}
