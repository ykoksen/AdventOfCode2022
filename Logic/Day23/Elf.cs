namespace Logic.Day23
{
    public enum Direction
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3,
    }

    public class Elf
    {
        public Position CurrentPosition { get; private set; }

        private Position _previousPosition;

        public Elf(Position position)
        {
            CurrentPosition = position;
            _previousPosition = position;
        }

        public Position CalculateMove(Positions positions, Direction[] directions)
        {
            Position? newPosition = null;

            var north = CurrentPosition with { Y = CurrentPosition.Y - 1 };
            var anyNorth = positions.Any(north.PostionsBeside());

            var south = CurrentPosition with { Y = CurrentPosition.Y + 1 };
            var anySouth = positions.Any(south.PostionsBeside());

            var west = CurrentPosition with { X = CurrentPosition.X - 1 };
            var anyWest = positions.Any(west.PositionsAboveUnder());

            var east = CurrentPosition with { X = CurrentPosition.X + 1 };
            var anyEast = positions.Any(east.PositionsAboveUnder());

            if (anyNorth || anySouth || anyWest || anyEast)
            {
                foreach (var direction in directions)
                {
                    switch (direction)
                    {
                        case Direction.North:
                            if (!anyNorth)
                                newPosition = north;
                            break;

                        case Direction.South:
                            if (!anySouth)
                                newPosition = south;
                            break;
                        case Direction.East:
                            if (!anyEast)
                                newPosition = east;
                            break;
                        case Direction.West:
                            if (!anyWest)
                                newPosition = west;
                            break;
                    }

                    if (newPosition != null)
                        break;
                }
            }

            _previousPosition = CurrentPosition;

            if (newPosition != null)
            {
                CurrentPosition = newPosition.Value;
            }

            return CurrentPosition;
        }

        public void MoveBack()
        {
            CurrentPosition = _previousPosition;
        }

        public override string ToString()
        {
            return CurrentPosition.ToString();
        }
    }

    public record struct Position(int X, int Y)
    {
        public Position[] PositionsAboveUnder() => new Position[]
        {
            this with { Y = Y -1},
            this,
            this with { Y = Y + 1 },
        };

        public Position[] PostionsBeside() => new Position[]
        {
            this with {X = X-1},
            this,
            this with {X =X+1},
        };
    }

    public class Positions : IEquatable<Positions>
    {
        private readonly Dictionary<Position, Elf> _positions;

        public int Count => _positions.Count;

        public IEnumerable<Elf> Elves => _positions.Values;

        public Positions()
        {
            _positions = new Dictionary<Position, Elf>();
        }

        public Positions(List<bool[]> positions)
        {
            _positions = new Dictionary<Position, Elf>();

            for (int y = 0; y < positions.Count; y++)
            {
                var array = positions[y];
                for (int x = 0; x < array.Length; x++)
                {
                    if (array[x])
                    {
                        _positions.Add(new Position(x, y), new Elf(new Position(x, y)));
                    }
                }
            }
        }

        public bool TryGetValue(Position newPosition, out Elf? existingElf) => _positions.TryGetValue(newPosition, out existingElf);

        public bool Any(Position[] positions)
        {
            return positions.Any(x => _positions.ContainsKey(x));
        }

        public bool Equals(Positions? other)
        {
            if (other == null || other.Count != this.Count)
                return false;

            foreach (var position in other._positions.Keys)
            {
                if (!_positions.ContainsKey(position))
                    return false;
            }

            return true;
        }

        public Elf? this[Position p]
        {
            get
            {
                _positions.TryGetValue(p, out Elf? elf);
                return elf;
            }

            set
            {
                if (value == null)
                    _positions.Remove(p);
                else
                    _positions[p] = value;
            }

        }
    }
}
