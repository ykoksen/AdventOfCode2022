namespace Logic.day21
{
    public static class Solver
    {
        public static async Task<string> Solve()
        {
            var input = await Reader.ReadInput();

            return input["root"].Calculate(input).ToString();
        }

        public static async Task<string> Solve2()
        {
            var input = await Reader.ReadInput();

            var root = (CalculateExpression)input["root"];

            ((ValueExpression)input["humn"]).Value = 3759569926192;

            var left = input[root.Left].Calculate(input);
            var right = input[root.Right].Calculate(input);

            Console.WriteLine(left);

            Console.WriteLine(left - right);

            return "halløj";
        }
    }
}
