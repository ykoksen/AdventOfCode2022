using System.Collections.Immutable;

namespace Logic.Day12
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var map = await Parser.ReadInput();

            PriorityQueue<CurrentRun, int> priorityQueue = new ();
            priorityQueue.Enqueue(new CurrentRun(map.Start, 0, ImmutableList<Position>.Empty.Add(map.Start)), 0);

            int bestResult = int.MaxValue;
            var bestRun = ImmutableList<Position>.Empty;

            Dictionary<Position, int> visited = new ();

            while (priorityQueue.TryDequeue(out var run, out var _))
            {
                int newMoves = run.NumberOfMoves + 1;
                if (newMoves >= bestResult)
                    continue;

                foreach(var (c, d) in map.GetPossibleMoves(run.Position))
                {
                    if (visited.TryGetValue(c, out int moves))
                    {
                        if (moves <= newMoves)
                        { 
                            continue;
                        }
                    }

                    if (d == 0)
                    {
                        bestResult = newMoves;
                        bestRun = run.Route.Add(c);
                    }
                    else
                    {
                        priorityQueue.Enqueue(new CurrentRun(c, newMoves, run.Route.Add(c)), d + run.NumberOfMoves + 1);
                        visited[c] = newMoves;
                    }
                }
            }

            return bestResult.ToString();
        }

        public static async Task<string> Solve2()
        {
            var map = await Parser.ReadInput();

            
            int bestResult = int.MaxValue;
            //var bestRun = ImmutableList<Position>.Empty;

            foreach (Position start in map.ElevationAPoints)
            {
                PriorityQueue<CurrentRun, int> priorityQueue = new();
                priorityQueue.Enqueue(new CurrentRun(start, 0, ImmutableList<Position>.Empty.Add(start)), 0);

                Dictionary<Position, int> visited = new();

                while (priorityQueue.TryDequeue(out var run, out var _))
                {
                    int newMoves = run.NumberOfMoves + 1;
                    if (newMoves >= bestResult)
                        continue;

                    foreach (var (c, d) in map.GetPossibleMoves(run.Position))
                    {
                        if (visited.TryGetValue(c, out int moves))
                        {
                            if (moves <= newMoves)
                            {
                                continue;
                            }
                        }

                        if (d == 0)
                        {
                            bestResult = newMoves;
                            //bestRun = run.Route.Add(c);
                        }
                        else
                        {
                            priorityQueue.Enqueue(new CurrentRun(c, newMoves, run.Route.Add(c)), d + run.NumberOfMoves + 1);
                            visited[c] = newMoves;
                        }
                    }
                }
            }

            return bestResult.ToString();
        }
    }
}
