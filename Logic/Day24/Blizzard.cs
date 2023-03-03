namespace Logic.Day24
{
    public enum Direction { Up, Down, Left, Right }

    public interface IElement
    {
        int X { get; }

        int Y { get; }

        IElement Move(int maxX, int maxY);
    }

    public record struct Wall(int X, int Y) : IElement
    {
        public IElement Move(int maxX, int maxY)
        {
            return this;
        }
    }

    public record struct WindPosition(int X, int Y, Direction Direction) : IElement
    {
        public WindPosition Move(int maxX, int maxY) => Direction switch
        {
            Direction.Up => Y == 1 ? this with { Y = maxY } : this with { Y = Y - 1 },
            Direction.Down => Y == maxY ? this with { Y = 1 } : this with { Y = Y + 1 },
            Direction.Left => X == 1 ? this with { X = maxX } : this with { X = X - 1 },
            Direction.Right => X == maxX ? this with { X = 1 } : this with { X = X + 1 },
            _ => throw new NotImplementedException(),
        };

        IElement IElement.Move(int maxX, int maxY)
        {
            return Move(maxX, maxY);
        }
    }

    public record struct Position(int X, int Y)
    {
        public Position Move(Direction d) => d switch
        {
            Direction.Up => new Position (X,  Y - 1 ),
            Direction.Down => new Position (X,  Y + 1 ),
            Direction.Left => new Position(X - 1, Y),
            Direction.Right => new Position(X + 1, Y),
            _ => throw new NotImplementedException(),
        };

        public int Distance(Position other)
        {
            return Math.Abs(X-other.X) + Math.Abs(Y-other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 17 * X.GetHashCode() + Y.GetHashCode();
            }
        }
    }

    public readonly struct BlizzardMaze : IEquatable<BlizzardMaze>
    {
        public readonly bool[,] Maze { get; }

        public List<IElement> WindsAndWalls { get; }

        public BlizzardMaze(List<IElement> windsAndWalls, Position exit)
        {
            WindsAndWalls = windsAndWalls;
            Maze = new bool[exit.X + 2, exit.Y + 1];
            CreateMaze();
        }

        private void CreateMaze()
        {
            foreach (var e in WindsAndWalls)
            {
                Maze[e.X, e.Y] = true;
            }
        }

        public BlizzardMaze Move(Position exit)
        {
            var newList = WindsAndWalls.Select(e => e.Move(exit.X, exit.Y - 1)).ToList();
            
            return new BlizzardMaze(newList, exit);
        }

        public bool Equals(BlizzardMaze other)
        {
            if (this.WindsAndWalls.Count != other.WindsAndWalls.Count)
                return false;

            for(int i = 0;i< this.WindsAndWalls.Count;i++)
            {
                if (!WindsAndWalls[i].Equals(other.WindsAndWalls[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var e in WindsAndWalls)
                {
                    hash *= hash * 31 + e.GetHashCode();
                }

                return hash;
            }
        }
    }
}
