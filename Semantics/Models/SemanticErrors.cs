using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Models;

namespace Semantics.Models
{
    public class SemanticErrors<T>
    {
        private readonly T _result;
        private readonly Dictionary<IToken, HashSet<string>> _errors = new();

        public SemanticErrors(T result)
        {
            _result = result;
        }
        
        public Dictionary<IToken, IReadOnlySet<string>> Collect()
        {
            return _errors.ToDictionary(x => x.Key, x => (IReadOnlySet<string>)x.Value.ToImmutableHashSet());
        }

        public T Error(IToken token, string error)
        {
            if (!_errors.ContainsKey(token))
            {
                _errors[token] = new HashSet<string>();
            }

            _errors[token].Add(error);

            return _result;
        }
    }
}