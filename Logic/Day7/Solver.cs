using Logic.Day5;

namespace Logic.Day7
{
    public static class Solver
    {
        public static async Task<string> Solve1()
        {
            var fileSystem = await Parser.Read();

            return fileSystem.Directories.Values.Select(z => z.GetSize()).Where(x => x <= 100000).Sum().ToString();
        }

        public static async Task<string> Solve2()
        {
            var fileSystem = await Parser.Read();

            var neededSpace = 30000000;

            var totalSpace =  70000000;

            var currentFreeSpace = totalSpace - fileSystem.RootFolder.GetSize();

            var missingSpace = neededSpace - currentFreeSpace;

            var bestDirectory = fileSystem.RootFolder;
            var currentBestSize = bestDirectory.GetSize();

            foreach(var folder in fileSystem.Directories.Values)
            {
                var size = folder.GetSize();

                if (size < currentBestSize && size >= missingSpace)
                {
                    bestDirectory = folder;
                    currentBestSize = size;
                }
            }

            return currentBestSize.ToString();
        }
    }
}
