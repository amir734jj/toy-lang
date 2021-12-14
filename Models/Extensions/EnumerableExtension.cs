using System.Collections.Generic;
using System.Collections.Immutable;
using Models.Interfaces;
using Models.Utilities;

namespace Models.Extensions
{
    public static class EnumerableExtension
    {
        public static IValueCollection<T> AsValueSemantics<T>(this IEnumerable<T> list)
        {
            return new ImmutableListWithValueSemantics<T>(list.ToImmutableList());
        }
    }
}