using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day12
{
    public static class Parser
    {
        public static async Task<Map> ReadInput()
        {
            using var reader = InputLoader.LoadReader(12);

            List<List<char>> list = new ();
            List<Position> aChars = new ();

            var lineNumber = 0;
            Position start = new Position(0, 0);
            Position end = new Position(0, 0);

            while (!reader.EndOfStream)
            {
                var currentLine = new List<char>();
                list.Add(currentLine);

                var line = (await reader.ReadLineAsync())!;

                for(int x=0;x<line.Length;x++)
                {
                    if (line[x] == 'S')
                    {
                        start = new Position(x, lineNumber);
                        aChars.Add(start);
                        currentLine.Add('a');
                    }
                    else if (line[x] == 'E')
                    {
                        end = new Position(x, lineNumber);
                        currentLine.Add('z');
                    }
                    else
                    {
                        currentLine.Add(line[x]);
                        if (line[x] == 'a')
                        {
                            aChars.Add(new Position(x, lineNumber));
                        }
                    }                    
                }

                lineNumber++;
            }

            char[,] innerMap = new char[list[0].Count, list.Count];

            for(int y = 0; y < list.Count; y++)
            {
                for(int x= 0; x < list[0].Count; x++)
                {
                    innerMap[x, y] = list[y][x];
                }
            }

            return new Map(innerMap, start, end, aChars);
        }
    }
}
