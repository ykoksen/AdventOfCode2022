namespace Logic.Day5
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Reader.Read();

            var stacks = input.Stacks;

            foreach ( var move in input.Moves ) 
            {
                for ( var i = 0;i<move.Moves;i++)
                {
                    var c = stacks.Pop(move.FromStack);
                    stacks.Add(move.ToStack, c);
                }
            }

            List<char> chars = new();

            for(int i=1;i<=stacks.Count;i++)
            {
                chars.Add(stacks.Peek(i));
            }

            return new string(chars.ToArray());
        }

        public static async Task<string> Solve2()
        {
            var input = await Reader.Read();

            var stacks = input.Stacks;

            foreach (var move in input.Moves)
            {
                var tempStack = new Stack<char>();
                for (var i = 0; i < move.Moves; i++)
                {
                    tempStack.Push(stacks.Pop(move.FromStack));                    
                }

                foreach (var c in tempStack)
                    stacks.Add(move.ToStack, c);
            }

            List<char> chars = new();

            for (int i = 1; i <= stacks.Count; i++)
            {
                chars.Add(stacks.Peek(i));
            }

            return new string(chars.ToArray());
        }
    }
}
