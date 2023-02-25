namespace Logic.Day8
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public class TreeMap
    {
        private Dictionary<Direction, Tree> _map;

        public TreeMap() 
        { 
            _map = new Dictionary<Direction, Tree>();
        }

        public void Add(Direction d, Tree t)
        {
            _map[d] = t;
        }

        public Tree? this[Direction d] => _map.TryGetValue(d, out var back) ? back : null;

        public int GetHighestValue(Direction d)
        {
            return this[d]?.GetHighestValue(d) ?? 0;
        }

        public int ScenicScore(Direction d, int compareValue)
        {
            var n = this[d];
            if (n == null) 
                return 0;
            else if (compareValue <= n.Value)
                return 1;
            else 
                return 1 + n.Neighbours.ScenicScore(d, compareValue);
        }
    }

    public class Tree
    {
        public static readonly Direction[] Directions = {Direction.Down, Direction.Left, Direction.Right, Direction.Up};

        public TreeMap Neighbours { get; }

        public Tree()
        {
            Neighbours = new TreeMap();
        }

        public int Value { get; set; }

        private Dictionary<Direction, int> _highestMap = new();

        public int GetHighestValue(Direction d)
        {
            if (_highestMap.TryGetValue(d, out int result)) 
                return result;
            else
            {
                var back = Math.Max(Neighbours.GetHighestValue(d), Value);
                _highestMap[d] = back;
                return back;
            }
        }

        public bool Visible()
        {
            foreach (var d in Directions)
            {
                if (Neighbours[d] == null)
                    return true;

                if (Neighbours.GetHighestValue(d) < Value) 
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public int ScenicScore()
        {
            int back = 1;
            foreach(var d in Directions)
            {
                back *= Neighbours.ScenicScore(d, Value);
            }

            return back;
        }
    }
}
