namespace Logic.Day8
{
    public static class Reader
    {
        public static async Task<List<List<int>>> Read()
        {
            using var reader = InputLoader.LoadReader(8);

            var list = new List<List<int>>();

            while(!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                var row = new List<int>();
                list.Add(row);

                foreach(var c in line)
                {
                    row.Add(int.Parse(c.ToString()));
                }
            }

            return list;
        }
    }
}
