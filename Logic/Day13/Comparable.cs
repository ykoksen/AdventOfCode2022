namespace Logic.Day13
{
    public enum Result
    {
        Right,
        Wrong,
        NoDecision
    }

    public interface IItem
    {
        ItemList ToItemList();
    }

    public record struct Integer(int Value) : IItem
    {
        public ItemList ToItemList()
        {
            return new ItemList(new List<IItem> { this});
        }
    }

    public record class ItemList(List<IItem> List) : IItem
    {
        public ItemList ToItemList() => this;        
    }

    public record struct Pair(IItem Left, IItem Right);
}
