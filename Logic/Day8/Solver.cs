using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day8
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Reader.Read();

            var forest = ForestCreator.CreateForest(input);

            var value = forest.OfType<Tree>().Count(x => x.Visible());

            return value.ToString();
            
        }

        public static async Task<string> Solve2()
        {
            var input = await Reader.Read();

            var forest = ForestCreator.CreateForest(input);

            var value = forest.OfType<Tree>().Max(x => x.ScenicScore());

            return value.ToString();

        }
    }
}
