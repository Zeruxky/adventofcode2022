namespace AdventOfCode.Day8
{
    public class PartTwoSolver : ISolver<int>
    {
        public Day Day => Day.Eight;
        
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

            using (var reader = new TreeMapReader(stream, true))
            {
                var trees = await reader.ReadAllAsync(ct)
                    .ToArrayAsync(ct)
                    .ConfigureAwait(false);
                var map = new TreeMap(trees);
                var scenicScore = map.GetScenicScores().Max();
                return scenicScore;
            }
        }
    }
}