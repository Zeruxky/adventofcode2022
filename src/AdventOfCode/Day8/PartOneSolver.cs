namespace AdventOfCode.Day8
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Eight;
        
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

            using (var reader = new TreeMapReader(stream, true))
            {
                var treeMap = await reader.ReadAsync(ct).ConfigureAwait(false);
                var visibleTrees = treeMap.GetVisibleTrees();
                return visibleTrees.Count();
            }
        }
    }
}