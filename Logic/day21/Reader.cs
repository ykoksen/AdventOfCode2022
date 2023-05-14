using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Logic.Input;

namespace Logic.day21
{
    public static class Reader
    {
        public static async Task<Dictionary<string, Expression>> ReadInput()
        {
            using var reader = Loader.LoadReader(21);

            var back = new Dictionary<string, Expression>();

            while(!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                var name = line[0..4];

                if (char.IsDigit(line[6]))
                {
                    back[name] = new ValueExpression(name, int.Parse(line[6..]));
                }
                else
                {
                    var left = line[6..10];
                    var right = line[13..17];

                    var op = line[11];
                    var opType = op switch
                    {
                        '-' => ExpressionType.Subtract,
                        '+' => ExpressionType.Add,
                        '*' => ExpressionType.Multiply,
                        '/' => ExpressionType.Divide,
                        _ => throw new NotImplementedException()
                    };

                    back[name] = new CalculateExpression(name, left, right, opType);
                }
            }

            return back;
        }
    }
}
