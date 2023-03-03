namespace Logic.Day22
{
    public record struct Information(List<IInstruction> Instructions, MapPoint Map);

    public interface IInstruction { }

    public record struct Move(int Moves) : IInstruction
    { }

    public record struct Turn(Direction Direction) : IInstruction { }
}
