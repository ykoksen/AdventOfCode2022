using System.Diagnostics;

namespace Logic.Day19
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Parser.Parse();

            List<Task<int>> tasks = new();

            var watch = Stopwatch.StartNew();

            for (var i = 0;i<input.Count;i++)
            {
                var traverSer = new Traverser(input[i], 24);
                var multiply = i+1;
                tasks.Add(Task.Run(() => multiply * traverSer.Traverse()));
            }

            var results = await Task.WhenAll(tasks);

            watch.Stop();

            Console.WriteLine($"Time spent in seconds was {watch.Elapsed.TotalSeconds}");

            return results.Sum().ToString();
        }

        public static async Task<string> Solve2()
        {
            var input = await Parser.Parse();

            List<Task<int>> tasks = new();

            var watch = Stopwatch.StartNew();

            for (var i = 0; i < Math.Min(3, input.Count); i++)
            {
                var traverSer = new Traverser(input[i], 32);
                var multiply = i + 1;
                tasks.Add(Task.Run(() => traverSer.Traverse()));
            }

            var results = await Task.WhenAll(tasks);

            watch.Stop();

            Console.WriteLine($"Time spent in seconds was {watch.Elapsed.TotalSeconds}");

            return results.Aggregate(1, (x, y) => x * y).ToString();
        }
    }
}
