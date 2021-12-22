using System.Collections.Generic;
using System.Linq;

namespace Semantics.Models
{
    internal class Contour<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _table = new Dictionary<TKey, TValue>();
        private Contour<TKey, TValue> _parent;

        public Contour<TKey, TValue> Push()
        {
            return new Contour<TKey, TValue>
            {
                _parent = this
            };
        }

        public void Update(TKey name, TValue token)
        {
            _table[name] = token;
        }
        
        public bool Lookup(TKey name, out TValue token)
        {
            if (_table.ContainsKey(name))
            {
                token = _table[name];
                return true;
            }

            if (_parent != null)
            {
                // ReSharper disable once TailRecursiveCall
                return _parent.Lookup(name, out token);;
            }

            token = default;
            return false;
        }

        public Contour<TKey, TValue> Pop()
        {
            return _parent;
        }
    }
}