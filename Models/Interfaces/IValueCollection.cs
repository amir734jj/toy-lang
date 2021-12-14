using System;
using System.Collections.Immutable;

namespace Models.Interfaces
{
    public interface IValueCollection<T> : IImmutableList<T>, IEquatable<IValueCollection<T>>
    {
    }
}