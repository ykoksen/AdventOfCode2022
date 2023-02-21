namespace Logic.Day10
{
    public abstract class Instruction
    {
        public abstract int Cycles { get; }

        public abstract void Run(ValueHolder value);
    }

    public class NoopInstruction : Instruction
    {
        public override int Cycles => 1;

        public override void Run(ValueHolder value) { }
    }

    public class AddxInstruction : Instruction
    {
        public int AddNumber { get; }

        public AddxInstruction(int addNumber)
        {
            AddNumber = addNumber;
        }

        public override int Cycles => 2;

        public override void Run(ValueHolder value)
        {
            value.Value += AddNumber;
        }
    }

    public class ValueHolder
    {
        public long Value { get; set; }
    }
}


