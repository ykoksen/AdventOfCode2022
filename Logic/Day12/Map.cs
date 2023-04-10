using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day12
{
    public struct Map
    {
        public Map(char[,] positions, Position start, Position end, IEnumerable<Position> elevationAPoints)
        {
            Positions = positions;
            Start = start;
            End = end;
            ElevationAPoints = elevationAPoints;
        }

        public readonly char[,] Positions { get; }

        public int this[Position position] => Positions[position.X, position.Y];

        public Position Start { get; }
        public Position End { get; }

        public IEnumerable<Position> ElevationAPoints { get; }

        public IEnumerable<(Position p, int distance)> GetPossibleMoves(Position position)
        {
            int currentValue = this[position];
            foreach (var p in GetNeighbours(position))
            {
                if (this[p]-1 <= currentValue)
                {
                    yield return (p, p.Distance(End));
                }
            }
        }

        public IEnumerable<Position> GetNeighbours(Position position)
        {
            if (position.X > 0)
            {
                yield return new Position(position.X - 1, position.Y);
            }
            if (position.X < Positions.GetLength(0) - 1)
            {
                yield return new Position(position.X + 1, position.Y);
            }
            if (position.Y > 0)
            {
                yield return new Position(position.X, position.Y - 1);
            }
            if (position.Y < Positions.GetLength(1) - 1)
            {
                yield return new Position(position.X, position.Y + 1);
            }
        }
    }

    public readonly record struct Position(int X, int Y)
    {
        public int Distance(Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }
    }
}
