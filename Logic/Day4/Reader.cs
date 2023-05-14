using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Input;

namespace Logic.Day4
{
    public static class Reader
    {
        public static async Task<List<(Sections first, Sections second)>> Read()
        {
            using var reader = Loader.LoadReader(4);

            var back = new List<(Sections first, Sections second)>();

            while (!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                var sections = line.Split(',');

                back.Add((Parse(sections[0]), Parse(sections[1])));
            }

            return back;
        }

        private static Sections Parse(string v)
        {
            var numbers = v.Split('-');
            return new Sections(int.Parse(numbers[0]), int.Parse(numbers[1]));
        }
    }
}
