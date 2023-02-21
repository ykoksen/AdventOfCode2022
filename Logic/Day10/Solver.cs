using System.Text;

namespace Logic.Day10
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var input = await Reader.Read();

            var cycleNumber = 1;
            var x = new ValueHolder { Value = 1};

            int nextCycle = 20;
            long sumSignal = 0;

            foreach(var op in input)
            {
                if (nextCycle >= cycleNumber && nextCycle < cycleNumber + op.Cycles)
                {
                    sumSignal += nextCycle * x.Value;
                    nextCycle += 40;
                }

                cycleNumber += op.Cycles;
                op.Run(x);
            }

            return sumSignal.ToString();
        }

        public static async Task<string> Solve2()
        {
            var input = await Reader.Read();

            var instruction = 0;
            var x = new ValueHolder { Value = 1 };
            int nextCycle = 0;
            StringBuilder line = new StringBuilder();
            int lineNumber = 0;

            Action nextAction = () => { };

            for (int cycle = 0; cycle < 241; cycle++)
            {
                if (cycle == nextCycle)
                {
                    nextAction();

                    if (input.Count > instruction)
                    {
                        var op = input[instruction++];
                        nextCycle += op.Cycles;
                        nextAction = () => op.Run(x);
                    }
                }

                var compareValue = cycle - 40 * lineNumber;
                if (compareValue >= x.Value - 1 && compareValue <= x.Value + 1)
                {
                    line.Append("#");
                }
                else
                    line.Append(".");

                if ((cycle+1) % 40 == 0)
                {
                    Console.WriteLine(line.ToString());
                    line = new StringBuilder();
                    lineNumber++;
                }
            }

            return "End of program";
        }
    }
}
