using System.Collections.Immutable;

namespace Logic.Day12
{
    public readonly record struct CurrentRun(Position Position, int NumberOfMoves, ImmutableList<Position> Route);
}
