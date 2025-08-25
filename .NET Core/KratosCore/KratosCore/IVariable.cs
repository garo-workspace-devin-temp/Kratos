using System;

namespace KratosCore
{
    public interface IVariable<T>
    {
        string Name { get; }
        T DefaultValue { get; }
        Type ValueType { get; }
    }
}
