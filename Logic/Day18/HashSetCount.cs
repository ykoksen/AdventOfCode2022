using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logic.Day18.HashSetCount;

namespace Logic.Day18
{
    public class HashSetCount : IEnumerable<(Cube cube, Counter counter)>
    {
        private Dictionary<Cube, Counter> _internalSet;

        public IEnumerable<Cube> Cubes => _internalSet.Keys;

        public HashSetCount()
        {
            _internalSet = new Dictionary<Cube, Counter>();
        }

        public int Count => _internalSet.Values.Aggregate(0, (x, y) => x + y.Value);

        public void Add(Cube add)
        {
            if (_internalSet.TryGetValue(add, out Counter? value))
            {
                value.Value++;
            }
            else
            {
                _internalSet[add] = new Counter { Value = 1 };
            }
        }

        public void SetDirect(Cube add, int value)
        {
            _internalSet[add] = new Counter { Value = value };
        }

        public bool Remove(Cube cube) => _internalSet.Remove(cube);

        public int GetCount(Cube cube)
        {
            return _internalSet.TryGetValue(cube, out Counter? value) ? value.Value : 0;
        }

        public bool Contains(Cube cube) => _internalSet.ContainsKey(cube);

        public IEnumerator<(Cube cube, Counter counter)> GetEnumerator()
        {
            foreach (var kvp in _internalSet)
            {
                yield return (kvp.Key, kvp.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class Counter
        {
            public int Value { get; set; }
        }
    }


}
