using System.Linq.Expressions;

namespace Logic.day21
{
    public abstract class Expression
    {
        public string Name { get; }

        protected Expression(string name)
        {
            Name = name;
        }

        public abstract long Calculate(Dictionary<string, Expression> expressions);
    }

    public class ValueExpression : Expression
    {
        public ValueExpression(string name, int value) : base(name)
        {
            Value = value;
        }

        public long Value { get; set; }

        public override long Calculate(Dictionary<string, Expression> expressions)
        {
            return Value;
        }
    }

    public class CalculateExpression : Expression
    {
        public string Left { get; }

        public string Right { get; }

        public ExpressionType ExpressionType { get; }

        public CalculateExpression(string name, string left, string right, ExpressionType expressionType) : base(name)
        {
            Left = left;
            Right = right;
            ExpressionType = expressionType;
        }

        public override long Calculate(Dictionary<string, Expression> expressions)
        {
            long left = expressions[Left].Calculate(expressions);
            long right = expressions[Right].Calculate(expressions);

            return ExpressionType switch
            {
                ExpressionType.Add =>  left + right,
                ExpressionType.Subtract => left - right,
                ExpressionType.Multiply => left * right,
                ExpressionType.Divide => left / right,
                _ => throw new NotSupportedException()
            };
        }
    }
}
