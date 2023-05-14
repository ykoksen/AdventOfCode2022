using System.Collections.Concurrent;
using Logic.Input;

namespace Logic.Day5
{
    public static class Reader
    {
        public static async Task<Package> Read()
        {
            var input = Loader.LoadReader(5);

            var stacks = new Stacks();
            var moves = new List<Move>();

            while(!input.EndOfStream)
            {
                var line = (await input.ReadLineAsync())!;
                var numberOFColums = -1;

                if(line.Contains('['))
                {
                    if (numberOFColums < 0)
                    {
                        numberOFColums = (line.Length+1)/4;
                    }

                    for (int i = 1; i <= numberOFColums; i++) {
                        var value = line[i * 4 - 3];
                        if (value != ' ')
                            stacks.Add(i, value);
                    }
                }
                else if (line.Contains("move")) 
                { 
                    var splits = line.Split(' ');
                    moves.Add(new Move(int.Parse(splits[1]), int.Parse(splits[3]), int.Parse(splits[5])));
                }
            }

            stacks.ReverseAll();
            return new Package(stacks, moves);
        }
    }

    public record Package(Stacks Stacks, List<Move> Moves);
    
    public class Stacks
    {
        public ConcurrentDictionary<int, Stack<char>> _innerList = new ();

        public int Count => _innerList.Count;

        public Stacks() { 
        }

        public char Peek(int stackNumber) => _innerList[stackNumber].Peek();

        public void Add(int stackNumber,  char value)
        {
            var stack = _innerList.GetOrAdd(stackNumber, _ => new Stack<char>());
            stack.Push(value);
        }

        public char Pop(int stackNumber) => _innerList[stackNumber].Pop();

        public void ReverseAll()
        {
            foreach(var pair in _innerList)
            {
                var newStack = new Stack<char>(pair.Value);
                _innerList[pair.Key] = newStack;
            }
        }
    }

    public record struct Move(int Moves, int FromStack, int ToStack);
}
