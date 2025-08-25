using System;
using System.Collections.Generic;

namespace KratosCore
{
    public class BasicGeometry : IGeometry
    {
        public int Id { get; }
        public string GeometryType { get; }
        public int PointsCount => Points.Count;
        public IReadOnlyList<INode> Points { get; }

        public BasicGeometry(int id, string geometryType, IReadOnlyList<INode> points)
        {
            Id = id;
            GeometryType = geometryType ?? throw new ArgumentNullException(nameof(geometryType));
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }

        public virtual double Area()
        {
            return 0.0;
        }

        public virtual double Volume()
        {
            return 0.0;
        }

        public virtual void GetShapeFunctions(double[] localCoordinates, out double[] shapeFunctions)
        {
            shapeFunctions = new double[PointsCount];
            
            if (PointsCount == 2) // Line element
            {
                var xi = localCoordinates[0];
                shapeFunctions[0] = 0.5 * (1.0 - xi);
                shapeFunctions[1] = 0.5 * (1.0 + xi);
            }
            else if (PointsCount == 3) // Triangle
            {
                var xi = localCoordinates[0];
                var eta = localCoordinates[1];
                shapeFunctions[0] = 1.0 - xi - eta;
                shapeFunctions[1] = xi;
                shapeFunctions[2] = eta;
            }
            else if (PointsCount == 4) // Quadrilateral
            {
                var xi = localCoordinates[0];
                var eta = localCoordinates[1];
                shapeFunctions[0] = 0.25 * (1.0 - xi) * (1.0 - eta);
                shapeFunctions[1] = 0.25 * (1.0 + xi) * (1.0 - eta);
                shapeFunctions[2] = 0.25 * (1.0 + xi) * (1.0 + eta);
                shapeFunctions[3] = 0.25 * (1.0 - xi) * (1.0 + eta);
            }
            else
            {
                for (int i = 0; i < PointsCount; i++)
                {
                    shapeFunctions[i] = 1.0 / PointsCount;
                }
            }
        }

        public virtual void GetShapeFunctionDerivatives(double[] localCoordinates, out double[,] derivatives)
        {
            derivatives = new double[PointsCount, localCoordinates.Length];
            
            if (PointsCount == 2 && localCoordinates.Length == 1) // Line element
            {
                derivatives[0, 0] = -0.5;
                derivatives[1, 0] = 0.5;
            }
            else if (PointsCount == 3 && localCoordinates.Length == 2) // Triangle
            {
                derivatives[0, 0] = -1.0; derivatives[0, 1] = -1.0;
                derivatives[1, 0] = 1.0;  derivatives[1, 1] = 0.0;
                derivatives[2, 0] = 0.0;  derivatives[2, 1] = 1.0;
            }
            else if (PointsCount == 4 && localCoordinates.Length == 2) // Quadrilateral
            {
                var xi = localCoordinates[0];
                var eta = localCoordinates[1];
                
                derivatives[0, 0] = -0.25 * (1.0 - eta); derivatives[0, 1] = -0.25 * (1.0 - xi);
                derivatives[1, 0] = 0.25 * (1.0 - eta);  derivatives[1, 1] = -0.25 * (1.0 + xi);
                derivatives[2, 0] = 0.25 * (1.0 + eta);  derivatives[2, 1] = 0.25 * (1.0 + xi);
                derivatives[3, 0] = -0.25 * (1.0 + eta); derivatives[3, 1] = 0.25 * (1.0 - xi);
            }
        }

        public override string ToString()
        {
            return $"{GeometryType} Geometry {Id} with {PointsCount} points";
        }
    }
}
