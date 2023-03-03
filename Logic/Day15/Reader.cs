using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day15
{
    public static class Reader
    {
        public static async Task<List<Sensor>> Read()
        {
            using var reader = InputLoader.LoadReader(15);

            var back = new List<Sensor>();

            while (!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                var split = line.Split('=', ',', ':');

                back.Add(new Sensor(new Position(int.Parse(split[1]), int.Parse(split[3])), new Position(int.Parse(split[^3]), int.Parse(split[^1]))));
            }

            return back;
        }
    }

    public record struct Position(int X, int Y)
    {
        public int Distance(Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }
    }

    public record struct Sensor(Position Position, Position Beacon)
    {
        private int? _distance;

        public int Distance
        {
            get
            {
                if (_distance == null)
                    _distance = Position.Distance(Beacon);

                return _distance.Value;
            }
        }
    }
}

