using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Collection
{
    public class DictionaryList<T, TY>
    {
        private readonly Dictionary<T, List<TY>> _dictionary = new();
        public void Add(T key, TY value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, new List<TY>());
            }

            _dictionary[key].Add(value);
        }

        public List<TY> this[T key] => _dictionary[key];

        public bool TryGetValue(T key, out List<TY> value)
        {
            return _dictionary.TryGetValue(key, out value);
        }
    }
}
