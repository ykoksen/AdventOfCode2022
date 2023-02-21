using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day18
{
    public static class Reader
    {
        public static async Task<List<Cube>> ReadCubes()
        {
            using var reader = InputLoader.LoadReader(18);

            var back = new List<Cube>();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var numbers = line!.Split(",");
                back.Add(new Cube(int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2])));
            }

            return back;
        }
    }
}
