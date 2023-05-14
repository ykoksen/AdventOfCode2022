using Logic.Input;

namespace Logic.Day13
{
    public static class Parser
    {
        public static async Task<List<Pair>> Read()
        {
            using var reader = Loader.LoadReader(13);

            var pairs = new List<Pair>();

            while(!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;
                var line2 = (await reader.ReadLineAsync())!;

                pairs.Add(new Pair(Parse(line), Parse(line2)));

                await reader.ReadLineAsync();
            }

            return pairs;
        }

        public static async Task<NeedTask2> Read2()
        {
            using var reader = Loader.LoadReader(13);

            var pairs = new SortedSet<IItem>(new ItemComparer());

            while (!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;
                var line2 = (await reader.ReadLineAsync())!;

                pairs.Add(Parse(line));
                pairs.Add(Parse(line2));

                await reader.ReadLineAsync();
            }

            var nr2 = new ItemList(new List<IItem> { new ItemList(new List<IItem> { new Integer(2) }) });
            var nr6 = new ItemList(new List<IItem> { new ItemList(new List<IItem> { new Integer(6) }) });
            pairs.Add(nr2);
            pairs.Add(nr6);

            return new NeedTask2(pairs, nr2, nr6);
        }

        private class ItemComparer : IComparer<IItem>
        {
            public int Compare(IItem? x, IItem? y)
            {
                if (x == null || y == null)
                    throw new Exception();

                return Solver.Compare(x, y) switch
                {
                    Result.Right => -1,
                    Result.Wrong => 1,
                    Result.NoDecision => 0,
                    _ => throw new NotImplementedException(),
                };
            }
        }

        public record struct NeedTask2(SortedSet<IItem> Items, IItem Nr2, IItem Nr6);

        private static IItem Parse(string line)
        {
            Stack<List<IItem>> openLists = new();

            var currentIndex = 0;

            while(currentIndex < line.Length)
            {
                if (line[currentIndex] == '[')
                {
                    openLists.Push(new List<IItem>());
                    currentIndex++;                    
                }
                else if (line[currentIndex] == ']')
                {
                    var doneList = new ItemList(openLists.Pop());

                    if (openLists.TryPeek(out var parent))
                    {
                        parent.Add(doneList);
                        currentIndex++;
                    }
                    else
                    {
                        return doneList;
                    }
                }
                else if (line[currentIndex] == ',')
                {
                    currentIndex++;
                    continue;
                }
                else
                {
                    var nextComma = line.IndexOf(',', currentIndex);
                    var nextEndBracket = line.IndexOf("]", currentIndex);

                    string nextNumber;
                    if (nextComma == -1 || nextEndBracket < nextComma)
                    {
                        nextNumber = line[currentIndex..nextEndBracket];
                        currentIndex = nextEndBracket;
                    }
                    else
                    {
                        nextNumber = line[currentIndex..nextComma];
                        currentIndex = nextComma + 1;
                    }

                    openLists.Peek().Add(new Integer(int.Parse(nextNumber)));                    
                }
            }

            throw new Exception("Thats not right");
        }
    }
}
