namespace AdventOfCode.Day7
{
    public class PartTwoSolver : ISolver<int>
    {
        private const int TotalDiskSpace = 70_000_000;
        private const int MinimumDiskSpace = 30_000_000;

        public Day Day => Day.Seven;
        
        public Part Part => Part.Two;
        
        public async Task<int> SolveAsync(Stream stream, CancellationToken ct)
        {
            if (stream.Length == 0)
            {
                return 0;
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read from write-only stream.", nameof(stream));
            }

            using (var reader = new TerminalOutputReader(stream, leaveOpen: true))
            {
                var fileSystem = await reader.ReadAsync(ct).ConfigureAwait(false);
                var unusedSpace = TotalDiskSpace - fileSystem.RootDirectory.Size;
                var requiredSpace = MinimumDiskSpace - unusedSpace;
                var directory = fileSystem.GetAllDirectories()
                    .Where(d => d.Size >= requiredSpace)
                    .OrderBy(d => d.Size)
                    .First();
                return directory.Size;
            }
        }
    }
}