using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Input;

namespace Logic.Day11
{
    public static class Parser
    {
        public static async Task<List<Monkey>> Parse()
        {
            using var reader = Loader.LoadReader(11);

            var monkeys = new List<Monkey>();

            while (!reader.EndOfStream)
            {
                var monkey = (await reader.ReadLineAsync())!;
                var startingItems = (await reader.ReadLineAsync())!;
                var operation = (await reader.ReadLineAsync())!;
                var test = (await reader.ReadLineAsync())!;
                var testTrue = (await reader.ReadLineAsync())!;
                var testFalse = (await reader.ReadLineAsync())!;
                var _ = (await reader.ReadLineAsync())!;
                
                var items = startingItems.Split(':')[1].Split(",").Select(x => new Item { Value = int.Parse(x.Trim()) }).ToList();
                var op = GetOperation(operation.Split('=')[1]);
                var testNumber = int.Parse(test.Split(" ")[^1]);
                var testTrueNumber = int.Parse(testTrue.Split(" ")[^1]);
                var testFalseNumber = int.Parse(testFalse.Split(" ")[^1]);

                monkeys.Add(new Monkey(items, op, testNumber, testTrueNumber, testFalseNumber));
            }

            return monkeys;
        }

        private static Expression GetOperation(string expressionText)
        {
            if (expressionText.Contains("*"))
            {
                var parts = expressionText.Split('*');
                return new OperationExpression(GetOperation(parts[0]), GetOperation(parts[1]), Operation.Multiply);
            }
            if (expressionText.Contains("+"))
            {
                var parts = expressionText.Split("+");
                return new OperationExpression(GetOperation(parts[0]), GetOperation(parts[1]), Operation.Add);
            }

            if (expressionText.Trim() == "old")
            {
                return new Old();
            }

            return new Constant(int.Parse(expressionText.Trim()));
        }
    }
}
