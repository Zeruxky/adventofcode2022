namespace AdventOfCode.Day7
{
    /// <summary>
    /// Provides functionality to interact with the elves device.
    /// </summary>
    public class FileSystem
    {
        public FileSystem()
        {
            this.RootDirectory = new Directory(Directory.RootDirectoryPath);
            this.CurrentDirectory = this.RootDirectory;
        }
        
        /// <summary>
        /// Gets the root <see cref="Directory"/>.
        /// </summary>
        public Directory RootDirectory { get; }

        /// <summary>
        /// Gets the current <see cref="Directory"/> to which the <see cref="FileSystem"/> points.
        /// </summary>
        public Directory CurrentDirectory { get; private set; }

        /// <summary>
        /// Creates a <see cref="Directory"/> with the provided <paramref name="name"/> and adds it to the
        /// <see cref="FileSystem"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Directory"/>.</param>
        public void CreateDirectory(string name)
        {
            var directory = new Directory(name, this.CurrentDirectory);
            this.CurrentDirectory.Add(directory);
        }

        /// <summary>
        /// Adds the provided <paramref name="file"/> to the <see cref="FileSystem"/>.
        /// </summary>
        /// <param name="file">The file to add.</param>
        public void Add(File file)
        {
            this.CurrentDirectory.Add(file);
        }

        /// <summary>
        /// Gets all directories within the <see cref="FileSystem"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of type <see cref="Directory"/> that contains all
        /// directories within the <see cref="FileSystem"/>.</returns>
        public IEnumerable<Directory> GetAllDirectories()
        {
            IEnumerable<Directory> GetAllDirectoriesRecursive(Directory directory)
            {
                yield return directory;
                
                if (directory.IsLeaf)
                {
                    yield break;
                }

                foreach (var subDirectory in directory.Subdirectories.SelectMany(GetAllDirectoriesRecursive))
                {
                    yield return subDirectory;
                }
            }

            return GetAllDirectoriesRecursive(this.RootDirectory);
        }
        
        /// <summary>
        /// Changes the directory to the provided <paramref name="directoryName"/>.
        /// </summary>
        /// <param name="directoryName">The name of the directory or ".." for moving one level out.</param>
        public void ChangeDirectory(string directoryName)
        {
            switch (directoryName)
            {
                case Directory.RootDirectoryPath:
                    this.CurrentDirectory = this.RootDirectory;
                    break;
                case "..":
                    this.CurrentDirectory = this.CurrentDirectory.Parent ?? this.RootDirectory;
                    break;
                default:
                    this.MoveIn(directoryName);
                    break;
            }
        }

        private void MoveIn(string directoryName)
        {
            var path = Path.Combine(this.CurrentDirectory.Path, directoryName);
            foreach (var subDirectory in this.CurrentDirectory.Subdirectories)
            {
                if (!subDirectory.Path.Equals(path))
                {
                    continue;
                }

                this.CurrentDirectory = subDirectory;
                return;
            }
            
            throw new InvalidOperationException($"Can not move to not existing directory {directoryName}.");
        }
    }
}