using System;

namespace KratosCore
{
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
