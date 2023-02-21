using System.Reflection;

namespace Logic
{
    public static class InputLoader
    {
        public static Stream Load(int day)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"Logic.Input.Day{day}.txt") ?? throw new Exception("Unable to load resource");
        }

        public static StreamReader LoadReader(int day)
        {
            return new StreamReader(Load(day));
        }
    }
}
