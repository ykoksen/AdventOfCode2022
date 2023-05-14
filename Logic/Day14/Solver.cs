using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day14
{
    internal class Solver
    {
        public static async Task Solve()
        {
           var rocks = await Parser.Parse();
           var rockPairsByDirection = rocks.Select(x => x.Order()).GroupBy(pair => pair.Direction).ToDictionary(group => group.Key, group => group.ToList());
           var horizontalPairs = rockPairsByDirection[Direction.Horizontal];
           var verticalPairs = rockPairsByDirection[Direction.Vertical];
           
            
        }
    }

    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public record struct Line(Point A, Point B)
    {
        public Direction Direction => A.X == B.X ? Direction.Vertical : Direction.Horizontal;

        public Line Order() => Direction switch
        {
            Direction.Horizontal => A.X <= B.X ? this : new Line(B, A),
            Direction.Vertical => A.Y <= B.Y ? this: new Line(B, A),
            _ => throw new NotImplementedException(),
        };

        public Point[] GetPoints()
        {
            Point[] back;
            if (Direction == Direction.Horizontal)
            {
                back = new Point[B.X - A.X];
                for(int i=A.X;i<=B.X;i++)
                {
                    back[i-A.X] = new Point(i, B.Y);
                }
            }
            else
            {
                back = new Point[B.Y - A.Y];
                for (int i=A.Y;i<=B.Y;i++)
                {
                    back[i-A.Y] = new Point(i, B.X);
                }
            }

            return back;
        }
    }
}
