using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day11
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var monkeys = await Parser.Parse();
            int divideWith = 3;

            for (int round = 1; round <= 20; round++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (Item item in monkey.Items)
                    {
                        monkey.MonkeyBusiness++;

                        item.Value = monkey.Operation.GetWorryLevel(item.Value) / divideWith;
                        if (item.Value % monkey.Test == 0)
                        {
                            monkeys[monkey.TestTrue].Items.Add(item);
                        }
                        else
                        {
                            monkeys[monkey.TestFalse].Items.Add(item);
                        }
                    }


                    monkey.Items.Clear();
                }

                if (round % 100 == 0)
                    Console.WriteLine($"At round: {round}");
            }

            var businesses = monkeys.Select(x => x.MonkeyBusiness).OrderDescending().ToArray();

            return (businesses[0] * businesses[1]).ToString();
        }

        public static async Task<string> Solve2()
        {
            var monkeys = await Parser.Parse();
            var divisor = monkeys.Select(x => x.Test).Aggregate((x, y) => x * y);

            for (int round = 1; round <= 10000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (Item item in monkey.Items)
                    {
                        monkey.MonkeyBusiness++;

                        item.Value = monkey.Operation.GetWorryLevel(item.Value) % divisor;
                        if (item.Value % monkey.Test == 0)
                        {
                            monkeys[monkey.TestTrue].Items.Add(item);
                        }
                        else
                        {
                            monkeys[monkey.TestFalse].Items.Add(item);
                        }
                    }


                    monkey.Items.Clear();
                }

                if (round % 100 == 0)
                    Console.WriteLine($"At round: {round}");
            }

            var businesses = monkeys.Select(x => x.MonkeyBusiness).OrderDescending().ToArray();

            return (businesses[0] * businesses[1]).ToString();
        }
    }
}
