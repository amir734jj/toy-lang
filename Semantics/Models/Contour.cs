using System.Collections.Generic;
using Models;

namespace Semantics.Models
{
    internal class Contour
    {
        private readonly IDictionary<string, Token> _table = new Dictionary<string, Token>();

        private Contour _parent;

        public Contour Push()
        {
            return new Contour
            {
                _parent = this
            };
        }

        public void Update(string name, Token token)
        {
            _table[name] = token;
        }
        
        public bool Lookup(string name, out Token token)
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

            token = null;
            return false;
        }

        public Contour Pop()
        {
            return _parent;
        }
    }
}