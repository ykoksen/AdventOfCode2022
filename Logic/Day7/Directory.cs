namespace Logic.Day5
{
    public class Directory
    {
        public Directory? Parent { get; }

        public string Name { get; }

        public string FullName => (Parent?.FullName ?? string.Empty) + $"/{Name}";

        public Dictionary<string, Directory> SubFolders { get; } = new ();

        public List<File> Files { get; } = new List<File>();

        public Directory(Directory? parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public long GetSize()
        {
            return Files.Sum(x => x.FileSize) + SubFolders.Values.Sum(y => y.GetSize());
        }
    }

    public record File (long FileSize, string Filename);

    public class FileSystem
    {
        public Directory RootFolder { get; } = new Directory(null, "/");

        public Dictionary<string, Directory> Directories { get; } = new Dictionary<string, Directory> ();
    }
}
