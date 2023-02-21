using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day10
{
    public static class Reader
    {
        public static async Task<List<Instruction>> Read()
        {
            using var reader = InputLoader.LoadReader(10);

            var back = new List<Instruction>();

            while (!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                Instruction operation = line[0..4] switch
                {
                    "noop" => new NoopInstruction(),
                    "addx" => new AddxInstruction(int.Parse(line[5..])),
                    _ => throw new NotSupportedException()
                };

                back.Add(operation);
            }

            return back;
        }
    }
}