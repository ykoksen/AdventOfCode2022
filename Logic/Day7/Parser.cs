using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day5
{
    public static class Parser
    {
        public static async Task<FileSystem> Read()
        {
            using var reader = InputLoader.LoadReader(7);

            if (await reader.ReadLineAsync() != "$ cd /")
                throw new Exception("Unexpected first line");
            
            FileSystem back = new ();
            var currentFolder = back.RootFolder;

            while(!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                string[] split = line.Split(' ');

                switch (split)
                {
                    case ["dir", var name]:
                        var newDir = new Directory(currentFolder, name);
                        currentFolder.SubFolders.Add(name, newDir);
                        back.Directories.Add(name, newDir);
                        break;
                    case ["$", "cd", ".."]:
                        currentFolder = currentFolder.Parent ?? currentFolder;
                        break;
                    case ["$", "cd", var name]:
                        currentFolder = currentFolder.SubFolders[name];
                        break;
                    case ["$", "ls"]:
                        break;
                    case [var number, var fileName]:
                        currentFolder.Files.Add(new File(long.Parse(number), fileName));
                        break;
                }
            }

            return back;
        }
    }
}
