using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day24
{
    public static class Solver
    {
        private static readonly Direction[] Directions = {Direction.Up, Direction.Down, Direction.Left, Direction.Right};

        public static async Task<string> Solve1()
        {
            var maze = await Parser.Read();

            var start = new Position(1, 0);
            var exit = new Position(maze.Maze.GetLength(0) - 2, maze.Maze.GetLength(1) - 1);


            var provider = MazeProvider.Generate(maze, exit);
            Traverser t = new Traverser(start, exit, provider);

            return t.Traverse().ToString();            
        }

        public static async Task<string> Solve2()
        {
            var maze = await Parser.Read();

            var start = new Position(1, 0);
            var exit = new Position(maze.Maze.GetLength(0) - 2, maze.Maze.GetLength(1) - 1);


            var provider = MazeProvider.Generate(maze, exit);
            Traverser t = new Traverser(start, exit, provider);
            int endOfFirst = t.Traverse();

            var provider2 = MazeProvider.Generate(provider[endOfFirst], exit);
            Traverser t2 = new Traverser(exit, start, provider2);
            int endOfSecond = t2.Traverse();

            var provider3 = MazeProvider.Generate(provider2[endOfSecond], exit);
            Traverser t3 = new Traverser(start, exit, provider3);
            int endOfThird = t3.Traverse();

            return (endOfFirst + endOfSecond + endOfThird).ToString();
        }

        ////public static void PrintMaze(BlizzardMaze maze)
        ////{
        ////    StringBuilder builder = new StringBuilder();
        ////    for (int y = 0; y < maze.Maze.GetLength(1); y++) 
        ////    {
        ////        for(int x = 0; x < maze.Maze.GetLength(0); x++)
        ////        {
        ////            builder.Append(maze.Maze[x, y] switch
        ////            {
        ////                Wall _ => '#',
        ////                WindPosition w => w.Direction switch
        ////                {
        ////                    Direction.Up => '^',
        ////                    Direction.Down => 'v',
        ////                    Direction.Left => '<',
        ////                    Direction.Right => '>',
        ////                    _ => throw new NotImplementedException(),
        ////                },
        ////                _ => '.'
        ////            });
        ////        }

        ////        builder.AppendLine();
        ////    }

        ////    Console.WriteLine(builder.ToString());
        ////}
    }

    public record struct Situation(Position P, int Moves);
    
    public readonly record struct SituationKey
    {
        public int PeriodMoves { get; }

        public Position Position { get; }

        public SituationKey(Situation s, int period)
        {
            PeriodMoves = s.Moves % period;
            Position = s.P;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 17 * PeriodMoves.GetHashCode() + Position.GetHashCode();
            }
        }
    }
}
