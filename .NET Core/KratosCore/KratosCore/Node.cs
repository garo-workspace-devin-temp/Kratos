using System;
using System.Collections.Generic;

namespace KratosCore
{
    public class Node : INode
    {
        private readonly Dictionary<string, object> _data;

        public int Id { get; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Node(int id, double x = 0.0, double y = 0.0, double z = 0.0)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            _data = new Dictionary<string, object>();
        }

        public T GetValue<T>(IVariable<T> variable)
        {
            if (variable == null)
                throw new ArgumentNullException(nameof(variable));

            if (_data.TryGetValue(variable.Name, out var value))
            {
                return (T)value;
            }

            return variable.DefaultValue;
        }

        public void SetValue<T>(IVariable<T> variable, T value)
        {
            if (variable == null)
                throw new ArgumentNullException(nameof(variable));

            _data[variable.Name] = value;
        }

        public bool HasValue<T>(IVariable<T> variable)
        {
            if (variable == null)
                throw new ArgumentNullException(nameof(variable));

            return _data.ContainsKey(variable.Name);
        }

        public double DistanceTo(INode other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var dx = X - other.X;
            var dy = Y - other.Y;
            var dz = Z - other.Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public INode Clone()
        {
            var clonedNode = new Node(Id, X, Y, Z);
            foreach (var kvp in _data)
            {
                clonedNode._data[kvp.Key] = kvp.Value;
            }
            return clonedNode;
        }

        public override string ToString()
        {
            return $"Node {Id}: ({X:F3}, {Y:F3}, {Z:F3})";
        }
    }
}
