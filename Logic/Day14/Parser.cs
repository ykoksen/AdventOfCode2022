
using System.Collections.Immutable;

namespace Logic.Day14
{
    public static class Parser
    {
        public static async Task<List<Line>> Parse()
        {
            using var reader = Input.Loader.LoadReader(14);

            var result = new List<Line>();

            while (!reader.EndOfStream)
            {
                var stringLine = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(stringLine))
                {
                    continue;
                }

                // split into points - then split into x and y, then parse to int
                var rockLines = stringLine.Replace(">", string.Empty).Split('-').Select(point => point.Split(',')).Select(coordinates => new Point(int.Parse(coordinates[0].Trim()), int.Parse(coordinates[1].Trim()))).ToList();
                for (int i = 0; i <= rockLines.Count; i++)
                {
                    result.Add(new Line(rockLines[i], rockLines[i+1]));
                }                
            }

            return result;
        }
    }

    public record struct Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new (a.X + b.X, a.Y + b.Y);
    }

    public record struct Rock(ImmutableList<Point> RockLines);
}
