using Logic.Input;

namespace Logic.Day23
{
    public static class Reader
    {
        public static async Task<List<bool[]>> Read()
        {
            using var reader = Loader.LoadReader(23);

            var back = new List<bool[]>();

            while(!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                back.Add(line.Select(x => x == '#').ToArray());                
            }

            return back;
        }
    }
}
