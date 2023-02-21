using System.Text;

namespace Logic.Day23
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Reader.Read();

            var positions = new Positions(input);

            var directions = new Direction[] { Direction.North, Direction.South, Direction.West, Direction.East };

            CalculateFreeSpaces(positions);

            for (int i = 0; i < 10; i++)
            {
                var newPositions = new Positions();

                var actions = new List<Action>();

                foreach (var elf in positions.Elves)
                {
                    var newPosition = elf.CalculateMove(positions, directions);

                    if (newPositions.TryGetValue(newPosition, out var existingElf))
                    {
                        actions.Add(() =>
                        {
                            existingElf!.MoveBack();
                            newPositions[newPosition] = null;
                            newPositions[existingElf.CurrentPosition] = existingElf;
                        });

                        elf.MoveBack();

                        newPositions[elf.CurrentPosition] = elf;
                    }
                    else
                    {
                        newPositions[newPosition] = elf;
                    }
                }

                foreach (var action in actions)
                    action();

                positions = newPositions;
                directions = NewDirections(directions);

                CalculateFreeSpaces(positions);
            }

            return CalculateFreeSpaces(positions).ToString();
        }

        public static async Task<string> Solve2()
        {
            var input = await Reader.Read();

            var positions = new Positions(input);

            var directions = new Direction[] { Direction.North, Direction.South, Direction.West, Direction.East };

            CalculateFreeSpaces(positions);

            for (int i = 0; i < 10000000; i++)
            {
                var newPositions = new Positions();

                var actions = new List<Action>();

                foreach (var elf in positions.Elves)
                {
                    var newPosition = elf.CalculateMove(positions, directions);

                    if (newPositions.TryGetValue(newPosition, out var existingElf))
                    {
                        actions.Add(() =>
                        {
                            existingElf!.MoveBack();
                            newPositions[newPosition] = null;
                            newPositions[existingElf.CurrentPosition] = existingElf;
                        });

                        elf.MoveBack();

                        newPositions[elf.CurrentPosition] = elf;
                    }
                    else
                    {
                        newPositions[newPosition] = elf;
                    }
                }

                foreach (var action in actions)
                    action();

                if (positions.Equals(newPositions))
                {
                    return (i+1).ToString();
                }
                
                positions = newPositions;
                directions = NewDirections(directions);

                
            }

            return "oops";
        }

        private static int CalculateFreeSpaces(Positions positions)
        {
            int lowX = int.MaxValue;
            int lowY = int.MaxValue;
            int highX = int.MinValue;
            int highY= int.MinValue;

            foreach (var position in positions.Elves.Select(x => x.CurrentPosition))
            {
                if (position.X < lowX)
                    lowX = position.X;
                if (position.Y < lowY)
                    lowY = position.Y;
                if (position.X > highX)
                    highX = position.X;
                if(position.Y > highY)
                    highY = position.Y;
            }


            StringBuilder sb = new();
            for (int y = lowY; y <= highY; y++) 
            {
                for (int x = lowX; x <= highX; x++)
                {
                    sb.Append(positions[new Position(x, y)] == null ? '.' : '#');
                }

                sb.AppendLine();
            }

            Console.WriteLine();
            Console.WriteLine(sb.ToString());
            Console.WriteLine();



            return ((1 + highX- lowX) * (1 + highY - lowY)) - positions.Count;
        }

        public static Direction[] NewDirections(Direction[] input)
        {
            return new Direction[] { input[1], input[2], input[3], input[0] };
        }

    }
}
