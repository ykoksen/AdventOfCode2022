namespace Logic.Day22
{
    public static class Parser
    {
        public static async Task<Information> Parse()
        {
            using var reader = InputLoader.LoadReader(22);

            List<string> mapLines = new ();

            while(true)
            {
                var line = (await reader.ReadLineAsync())!;

                if (string.IsNullOrEmpty(line))
                    break;

                mapLines.Add(line);
            }

            List<IInstruction> instructions = new ();
            var endLine = (await reader.ReadLineAsync())!;
            int startNumberIndex = 0;
            int i = 0;
            for (; i < endLine.Length; i++) 
            {
                var currentChar = endLine[i];
                if (currentChar == 'L' || currentChar == 'R')
                {
                    instructions.Add(new Move(int.Parse(endLine[startNumberIndex..i])));
                    startNumberIndex = i+1;

                    instructions.Add(new Turn(currentChar == 'L' ? Direction.Left : Direction.Right));
                }
            }

            if (i <= endLine.Length)
                instructions.Add(new Move(int.Parse(endLine[startNumberIndex..i])));

            int maxWidth = mapLines.Max(x => x.Length);

            MapPoint[,] map = new MapPoint[maxWidth, mapLines.Count];

            for(int y=0; y < mapLines.Count; y++)
            {
                var row = mapLines[y];
                for(int x=0; x<row.Length;x++)
                {
                    switch (row[x])
                    {
                        case '#':
                            map[x, y] = new MapPoint(true, new Point(x, y)); 
                            break;
                        case '.':
                            map[x, y] = new MapPoint(false, new Point(x, y));
                            break;
                        case ' ':
                            break;
                        default:
                            throw new Exception();                            
                    }
                }
            }

            return new Information(instructions, SetupReferences(map));            
        }

        private static MapPoint SetupReferences(MapPoint[,] map)
        {
            MapPoint? back = null;

            for (int y = 0; y < map.GetLength(1); y++)
            {
                MapPoint? firstInLine = null;
                MapPoint? previous = null;

                for (int x = 0; x < map.GetLength(0); x++)
                {
                    var current = map[x, y];
                    if (current != null)
                    {
                        if (firstInLine == null)
                        {
                            previous = current;
                            firstInLine = current;
                            continue;
                        }

                        previous!.Right = current;
                        current.Left = previous;

                        previous = current;
                    }
                }

                previous!.Right = firstInLine!;
                firstInLine!.Left = previous;

                if (y == 0)
                    back = firstInLine;
            }

            for (int x = 0; x < map.GetLength(0); x++)                
            {
                MapPoint? firstInColumn = null;
                MapPoint? previous = null;

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    var current = map[x, y];
                    if (current != null)
                    {
                        if (firstInColumn == null)
                        {
                            previous = current;
                            firstInColumn = current;
                            continue;
                        }

                        previous!.Down = current;
                        current.Up = previous;

                        previous = current;
                    }
                }

                previous!.Down = firstInColumn!;
                firstInColumn!.Up = previous;
            }

            return back!;
        }
    }
}
