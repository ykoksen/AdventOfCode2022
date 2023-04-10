using System.Numerics;

namespace Logic.Day11
{
    public class Monkey
    {
        public Monkey(List<Item> items, Expression operation, int test, int testTrue, int testFalse)
        {
            Items = items;
            Operation = operation;
            Test = test;
            TestTrue = testTrue;
            TestFalse = testFalse;
        }

        public List<Item> Items { get; }

        public Expression Operation { get; }

        public int Test { get; }

        public int TestTrue { get; }
        public int TestFalse { get; }

        public long MonkeyBusiness { get; set; } = 0;
    }

    public abstract class Expression
    {
        public abstract long GetWorryLevel(long oldValue);
    }

    public class Constant : Expression
    {
        public int Value { get; }

        public Constant(int value)
        {
            Value = value;
        }

        public override long GetWorryLevel(long oldValue) => Value;
    }

    public class Old : Expression
    {
        public override long GetWorryLevel(long oldValue)
        {
            return oldValue;
        }
    }

    public class OperationExpression : Expression
    {
        public Expression Left { get; }
        public Expression Right { get; }
        public Operation Operation { get; }

        public OperationExpression(Expression left, Expression right, Operation operation)
        {
            Left = left;
            Right = right;
            Operation = operation;
        }

        public override long GetWorryLevel(long oldValue)
        {
            var left = Left.GetWorryLevel(oldValue);
            var right = Right.GetWorryLevel(oldValue);
            return Operation switch
            {
                Operation.Add => left + right,
                Operation.Multiply => left * right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum Operation
    {
        Add,
        Multiply,
    }

    public class Item
    {
        public long Value { get; set; }
    }
}