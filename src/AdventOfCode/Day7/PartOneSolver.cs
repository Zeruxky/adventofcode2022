namespace AdventOfCode.Day7
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Seven;
        
        public Part Part => Part.One;
        
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
                var totalSize = fileSystem.GetAllDirectories()
                    .Where(x => x.Size <= 100_000)
                    .Sum(x => x.Size);
                return totalSize;
            }
        }
    }
}