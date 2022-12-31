namespace AdventOfCode.Day4.PartOne
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Four;
        
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

            using (var reader = new SectionAssignmentPairReader(stream, true))
            {
                var pairs = reader.ReadAllAsync(ct);
                var overlappingPairs = await pairs
                    .SumAsync(p =>
                    {
                        if (p.First.Range.FullyContains(p.Second.Range))
                        {
                            return 1;
                        }

                        if (p.Second.Range.FullyContains(p.First.Range))
                        {
                            return 1;
                        }

                        return 0;
                    }, cancellationToken: ct)
                    .ConfigureAwait(false);
                return overlappingPairs;
            }
        }
    }
}