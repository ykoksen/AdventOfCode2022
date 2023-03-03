namespace Logic.Day22
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Parser.Parse();

            var current = input.Map;
            var instructions = input.Instructions;
            var direction = Direction.Right;

            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case Move m:
                        {
                            current = Move(current, direction, m.Moves);
                            break;
                        }
                    case Turn t:
                        {
                            direction = direction.Turn(t.Direction);
                            break;
                        }
                }
            }

            return (1000 * (current.Point.Y+1) + 4 * (current.Point.X+1) + (int)direction).ToString();
        }

        private static MapPoint Move(MapPoint current, Direction d, int m)
        {
            for (var i = 0; i < m; i++)
            {
                current = d switch
                {
                    Direction.Up => current.Up.IsWall ? current : current.Up,
                    Direction.Down => current.Down.IsWall ? current : current.Down,
                    Direction.Left => current.Left.IsWall ? current : current.Left,
                    Direction.Right => current.Right.IsWall ? current : current.Right,
                    _ => throw new NotImplementedException(),
                };
            }

            return current;
        }
    }
}
