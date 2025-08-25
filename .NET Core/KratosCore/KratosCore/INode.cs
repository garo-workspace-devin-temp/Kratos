using System;
using System.Collections.Generic;

namespace KratosCore
{
    public interface INode
    {
        int Id { get; }
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        
        T GetValue<T>(IVariable<T> variable);
        void SetValue<T>(IVariable<T> variable, T value);
        bool HasValue<T>(IVariable<T> variable);
        
        double DistanceTo(INode other);
        INode Clone();
    }
}
