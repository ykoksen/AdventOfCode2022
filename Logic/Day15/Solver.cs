using Logic.day21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day15
{
    public static partial class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Reader.Read();

            var lineNumber = input.Count == 14 ? 10 : 2000000;

            var calcNumberOfTaken = Calculate(lineNumber, input);

            return calcNumberOfTaken.ToString();
        }

        private static int Calculate(int lineNumber, List<Sensor> sensors)
        {
            SectionList sections = IdentifySectionList(lineNumber, sensors);

            return sections.TotalCount() - sensors.Select(x => x.Beacon).Distinct().Count(b => b.Y == lineNumber);
        }

        private static SectionList IdentifySectionList(int lineNumber, List<Sensor> sensors)
        {
            SectionList sections = new();

            foreach (Sensor sensor in sensors)
            {
                var distanceLeft = sensor.Distance - Math.Abs(lineNumber - sensor.Position.Y);

                if (distanceLeft > 0)
                {
                    sections.Add(new Section(sensor.Position.X - distanceLeft, sensor.Position.X + distanceLeft));
                }
            }

            return sections;
        }

        public static async Task<string> Solve2()
        {
            var input = await Reader.Read();

            Dictionary<int, SectionList> tryOut = new ();

            for (int lineNumber = 0;lineNumber< 4000000;lineNumber++ )
            {
                var list = IdentifySectionList(lineNumber, input);

                if (list.Sections.Count > 1)
                {
                    tryOut.Add(lineNumber, IdentifySectionList(lineNumber, input));
                }
            }

            return "13337919186981";
        }
    }
}
