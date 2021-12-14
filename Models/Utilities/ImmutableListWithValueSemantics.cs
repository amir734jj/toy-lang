using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Models.Extensions;
using Models.Interfaces;

namespace Models.Utilities
{
    internal class ImmutableListWithValueSemantics<T> : IValueCollection<T>
    {
        private readonly IImmutableList<T> _list;

        public ImmutableListWithValueSemantics(IImmutableList<T> list)
        {
            _list = list;
        }

        #region IImutableList implementation

        public T this[int index] => _list[index];

        public int Count => _list.Count;

        public IImmutableList<T> Add(T value)
        {
            return _list.Add(value).AsValueSemantics();
        }

        public IImmutableList<T> AddRange(IEnumerable<T> items)
        {
            return _list.AddRange(items).AsValueSemantics();
        }

        public IImmutableList<T> Clear()
        {
            return _list.Clear().AsValueSemantics();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
        {
            return _list.IndexOf(item, index, count, equalityComparer);
        }

        public IImmutableList<T> Insert(int index, T element)
        {
            return _list.Insert(index, element).AsValueSemantics();
        }

        public IImmutableList<T> InsertRange(int index, IEnumerable<T> items)
        {
            return _list.InsertRange(index, items).AsValueSemantics();
        }

        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
        {
            return _list.LastIndexOf(item, index, count, equalityComparer);
        }

        public IImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer)
        {
            return _list.Remove(value, equalityComparer).AsValueSemantics();
        }

        public IImmutableList<T> RemoveAll(Predicate<T> match)
        {
            return _list.RemoveAll(match).AsValueSemantics();
        }

        public IImmutableList<T> RemoveAt(int index)
        {
            return _list.RemoveAt(index).AsValueSemantics();
        }

        public IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
        {
            return _list.RemoveRange(items, equalityComparer).AsValueSemantics();
        }

        public IImmutableList<T> RemoveRange(int index, int count)
        {
            return _list.RemoveRange(index, count).AsValueSemantics();
        }

        public IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
        {
            return _list.Replace(oldValue, newValue, equalityComparer).AsValueSemantics();
        }

        public IImmutableList<T> SetItem(int index, T value)
        {
            return _list.SetItem(index, value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        public override bool Equals(object obj)
        {
            return Equals(obj as IValueCollection<T>);
        }

        public bool Equals(IValueCollection<T> other)
        {
            return this.SequenceEqual(other ?? ImmutableList<T>.Empty.AsValueSemantics());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Aggregate(19, (h, i) => h * 19 + i.GetHashCode());
            }
        }

        public override string ToString()
        {
            return @$"[{string.Join(",", _list)}]";
        }
    }
}