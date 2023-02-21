using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day4
{
    public static class Solver
    {
        public static async Task<string> Solve()
        {
            var count = 0;
            var input = await Reader.Read();

            foreach ((var first, var second) in input)
            {
                if (first.Start <= second.Start && first.End >= second.End)
                    count++;
                else if (second.Start <= first.Start && second.End >= first.End)
                    count++;
            }

            return count.ToString();
        }

        public static async Task<string> Solve2()
        {
            var count = 0;
            var input = await Reader.Read();

            foreach ((var first, var second) in input)
            {
                if (first.Start <= second.Start && first.End >= second.Start)
                    count++;
                else if (second.Start <= first.Start && second.End >= first.Start)
                    count++;
            }

            return count.ToString();
        }
    }
}
