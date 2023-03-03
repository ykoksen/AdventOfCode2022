namespace Logic.Day22
{
    public enum Direction { Right = 0, Down = 1, Left = 2, Up = 3}
    public class MapPoint
    {
        public Point Point { get; }

        public bool IsWall { get; }

        public MapPoint(bool isWall, Point point)
        {
            IsWall = isWall;
            Point = point;
        }

        public MapPoint Up { get; set; }
        public MapPoint Down { get; set; }
        public MapPoint Left { get; set; }
        public MapPoint Right { get; set; }

        public override string ToString()
        {
            return Point.ToString();
        }
    }

    public record struct Point(int X, int Y);

    public static class CurrentPosition
    {
        public static Direction Turn(this Direction facing, Direction change) => change switch
        {
            Direction.Left =>
                facing switch
                {
                    Direction.Up => Direction.Left,
                    Direction.Down => Direction.Right,
                    Direction.Left => Direction.Down,
                    Direction.Right => Direction.Up,
                    _ => throw new NotImplementedException(),
                }
            ,
            Direction.Right =>
                facing switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    Direction.Right => Direction.Down,
                    _ => throw new NotImplementedException(),
                }
            ,
            _ => throw new ArgumentException(),
        };
    }
}
