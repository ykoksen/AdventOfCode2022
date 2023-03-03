namespace Logic.Day24
{
    public class Traverser
    {
        private static readonly Direction[] Directions = { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

        public Position Entrance { get; }

        public Position Exit { get; }

        public MazeProvider MazeProvider { get; }

        public PriorityQueue<Situation, int> Queue { get; }

        private int _bestResult;

        private HashSet<SituationKey> Situations { get; }

        public Traverser(Position entrance, Position exit, MazeProvider provider)
        {
            Situations = new HashSet<SituationKey>();
            Entrance = entrance;
            Exit = exit;
            MazeProvider = provider;
            _bestResult = int.MaxValue;
            Queue = new PriorityQueue<Situation, int>();
            Queue.Enqueue(new Situation(entrance, 0), exit.Distance(entrance));
        }

        public int Traverse()
        {
            while (Queue.TryDequeue(out var element, out var minDistance))
            {
                var moves = element.Moves + 1;
                var maze = MazeProvider[moves];

                if (moves + minDistance > _bestResult)
                {
                    continue;
                }

                var s = new Situation(element.P, moves);
                if (!maze.Maze[element.P.X, element.P.Y] && Situations.Add(new SituationKey(s, MazeProvider.Period)))
                {
                    Queue.Enqueue(s, Exit.Distance(element.P));
                }

                foreach (var d in Directions)
                {
                    var newPosition = element.P.Move(d);

                    if (newPosition.Y < 0 || newPosition.Y >= maze.Maze.GetLength(1) || maze.Maze[newPosition.X, newPosition.Y])
                        continue;

                    var distance = Exit.Distance(newPosition);

                    if (distance == 0)
                    {
                        _bestResult = moves;
                        continue;
                    }

                    var newSituation = new Situation(newPosition, moves);
                    if (!Situations.Add(new SituationKey(newSituation, MazeProvider.Period)))
                        continue;

                    Queue.Enqueue(newSituation, distance);
                }
            }

            return _bestResult;
        }
    }

    public class MazeProvider
    {
        private Dictionary<int, BlizzardMaze> Mazes { get; }

        private readonly Position exit;
        public int Period { get; private set; }

        private MazeProvider(BlizzardMaze startMaze, Position exit)
        {
            Mazes = new Dictionary<int, BlizzardMaze>
            {
                { 0, startMaze }
            };

            this.exit = exit;
        }

        public static MazeProvider Generate(BlizzardMaze startMaze, Position exit)
        {
            var back = new MazeProvider(startMaze, exit);
            back.GenerateMazes();

            return back;
        }

        private void GenerateMazes()
        {
            int counter = 0;
            while (true)
            {
                var newMaze = Mazes[counter++].Move(exit);

                if (Mazes[0].Equals(newMaze))
                {
                    Period = counter;
                    break;
                }
                else
                {
                    Mazes[counter] = newMaze;
                }
            }
        }

        public BlizzardMaze this[int time] => Mazes[time % Period];

        public void CreateAll()
        {

        }

        private void Print(int time)
        {
            Console.WriteLine();
            Console.WriteLine($"New maze at Time: {time}");
            // Solver.PrintMaze(Mazes[time]);
        }
    }
}
