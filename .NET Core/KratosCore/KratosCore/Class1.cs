using System;
using System.Collections.Generic;

namespace KratosCore
{
    public interface IVariable<T>
    {
        string Name { get; }
        T DefaultValue { get; }
        Type ValueType { get; }
    }

    public class Variable<T> : IVariable<T>
    {
        public string Name { get; }
        public T DefaultValue { get; }
        public Type ValueType => typeof(T);

        public Variable(string name, T defaultValue = default(T))
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DefaultValue = defaultValue;
        }

        public override string ToString() => Name;
    }
}
