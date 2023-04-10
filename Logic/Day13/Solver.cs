namespace Logic.Day13
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Parser.Read();

            var result = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (Compare(input[i].Left, input[i].Right) == Result.Right)
                {
                    result += i + 1;
                }
            }

            return result.ToString();
        }

        public static async Task<string> Solve2()
        {
            var input = await Parser.Read2();

            var arary = input.Items.ToArray();

            int k1 = 0;
            int k2 = 0;

            for (int i = 0;i< arary.Length;i++)
            {
                if (arary[i] == input.Nr2)
                    k1 = i+1;
                else if (arary[i] == input.Nr6)
                    k2 = i+1;
            }

            return (k1 * k2).ToString();
        }

       

        public static Result Compare(IItem left, IItem right)
        {
            if (left is Integer leftInteger && right is Integer rightInteger)
            {
                return leftInteger.Value.CompareTo(rightInteger.Value) switch
                {
                    < 0 => Result.Right,
                    > 0 => Result.Wrong,
                    0 => Result.NoDecision,
                };
            }

            if (left is ItemList leftList && right is ItemList rightList)
            {
                for (int i = 0; i < Math.Min(leftList.List.Count, rightList.List.Count); i++)
                {
                    var possibleResult = Compare(leftList.List[i], rightList.List[i]);
                    if (possibleResult != Result.NoDecision)
                    {
                        return possibleResult;
                    }
                }

                return Compare(new Integer(leftList.List.Count), new Integer(rightList.List.Count));
            }

            return Compare(left.ToItemList(), right.ToItemList());
        }
    }
}
