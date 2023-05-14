using System.Collections.Immutable;

namespace Logic.Day14
{
    public class Rocks
    {
        private readonly ImmutableSortedDictionary<int, Line> _horizontalLines;

        private readonly ImmutableSortedDictionary<int, Line> _verticalLines;

        public Rocks(IReadOnlyList<Line> lines)
        {
            var horizon = new Dictionary<int, Line>();
            var vertical = new Dictionary<int, Line>();

            foreach (var line in lines)
            {
                if (line.Direction == Direction.Horizontal)
                {
                    horizon.Add(line.A.Y, line);
                }
                else
                {
                    vertical.Add(line.A.X, line);
                }                
            }

            _horizontalLines = horizon.ToImmutableSortedDictionary();
            _verticalLines = vertical.ToImmutableSortedDictionary();
        }

        //public bool LineIntersects(int X, out Line line)
        //{
        //    var possibleHorizontalLines

        //    if (_verticalLines.TryGetValue(X, out var verticalLine))
        //    {

        //    }
        //}
    }

    public class RockPoints
    {
        public Point Lowest { get; private set; }

        private readonly HashSet<Point> _points;

        public RockPoints()
        {
            _points = new HashSet<Point>();
            Lowest = new Point(0, int.MaxValue);
        }

        public bool Contains(Point point) => _points.Contains(point);

        public void Add(Point point)
        {
            _points.Add(point);

            if (Lowest.Y > point.Y)
                Lowest = point;
        }

        public void Add(Line line)
        {
            foreach (var point in line.GetPoints())
            {
                Add(point);
            }
        }
    }
}