namespace AdventOfCode.Day7
{
    /// <summary>
    /// Represents a directory within the elves <see cref="FileSystem"/>.
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// The path for the root directory.
        /// </summary>
        public const string RootDirectoryPath = "/";

        private readonly List<Directory> subdirectories;
        private readonly List<File> files;

        public Directory(string name, Directory? parent = default)
        {
            this.Name = name;
            this.subdirectories = new List<Directory>();
            this.files = new List<File>();
            this.Parent = parent;
        }
        
        /// <summary>
        /// Gets the name of the <see cref="Directory"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the path of the <see cref="Directory"/>.
        /// </summary>
        public string Path => this.IsRoot
            ? RootDirectoryPath
            : Day7.Path.Combine(this.Parent!.Path, this.Name);

        /// <summary>
        /// Gets the parent of the current <see cref="Directory"/> or <c>null</c> if no parent is given.
        /// </summary>
        public Directory? Parent { get; }

        /// <summary>
        /// Gets the subdirectories of the current <see cref="Directory"/>.
        /// </summary>
        public IReadOnlyCollection<Directory> Subdirectories => this.subdirectories;

        /// <summary>
        /// Gets the files of the current <see cref="Directory"/>.
        /// </summary>
        public IReadOnlyCollection<File> Files => this.files;
        
        /// <summary>
        /// Gets the size of the current <see cref="Directory"/>. The size results from the summed size of all
        /// directories containing within the current <see cref="Directory"/> and the summed size of all files
        /// containing within the current <see cref="Directory"/>.
        /// </summary>
        public int Size => this.subdirectories.Sum(d => d.Size) + this.files.Sum(f => f.Size);

        /// <summary>
        /// Indicates if the current <see cref="Directory"/> is a leaf directory. A leaf directory is a directory
        /// without any subdirectories.
        /// </summary>
        public bool IsLeaf => !this.subdirectories.Any();

        /// <summary>
        /// Indicates if the current <see cref="Directory"/> is the root directory. The root directory is
        /// the outermost directory.
        /// </summary>
        public bool IsRoot => this.Parent is null;

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Path;
        }

        /// <summary>
        /// Adds the provided <paramref name="directory"/> as a subdirectory of the current <see cref="Directory"/>.
        /// </summary>
        /// <param name="directory">The <see cref="Directory"/> to add.</param>
        public void Add(Directory directory)
        {
            if (this.subdirectories.Exists(d => d.Path.Equals(directory.Path)))
            {
                return;
            }
            
            this.subdirectories.Add(directory);
        }

        /// <summary>
        /// Adds the provided <paramref name="file"/> as a subdirectory of the current <see cref="File"/>.
        /// </summary>
        /// <param name="file">The <see cref="File"/> to add.</param>
        public void Add(File file)
        {
            if (this.files.Exists(f => f.Name.Equals(file.Name)))
            {
                return;
            }

            this.files.Add(file);
        }
    }
}