using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Models;

namespace Semantics.Models
{
    public class SemanticErrors
    {
        private readonly List<(IToken token, string error)> _errors = new();

        public Dictionary<IToken, ReadOnlyCollection<string>> Collect()
        {
            return _errors.GroupBy(x => x.token, x => x.error)
                .ToDictionary(x => x.Key, x => x.ToList().AsReadOnly());
        }

        public Unit Error(IToken token, string error)
        {
            _errors.Add((token, error));

            return Unit.Instance;
        }
    }
}