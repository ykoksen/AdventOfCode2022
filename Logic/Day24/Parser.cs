using Logic.Input;

namespace Logic.Day24
{
    public static class Parser
    {
        public static async Task<BlizzardMaze> Read()
        {
            using var input = Loader.LoadReader(24);

            List<IElement> elements = new ();
            Position entrance = new Position(1, 0);

            int y = -1;
            while(!input.EndOfStream)
            {
                var line = (await input.ReadLineAsync())!;
                y++;

                int x = -1;
                
                foreach(var c in line) 
                {
                    x++;
                    if (c == '#')
                    {
                        elements.Add(new Wall(x, y));
                        continue;
                    }

                    Direction? d = c switch
                    {
                        '<' => Direction.Left,
                        '^' => Direction.Up,
                        '>' => Direction.Right,
                        'v' => Direction.Down,
                        _ => null
                    };

                    if (d.HasValue)
                        elements.Add(new WindPosition(x, y, d.Value));
                }
            }

            var e = elements.Last();
            Position exit = new Position(e.X - 1, e.Y);

            return new BlizzardMaze(elements, exit);
        }
    }
}
