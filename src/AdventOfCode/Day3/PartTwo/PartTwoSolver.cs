namespace AdventOfCode.Day3.PartTwo
{
    public class PartTwoSolver : ISolver<int>
    {
        public Day Day => Day.Three;
        
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
            
            using (var reader = new RucksackListReader(stream, true))
            {
                var rucksacks = reader.ReadAllAsync(ct);
                var priority = await rucksacks
                    .ChunkAsync(3, ct)
                    .SumAsync(c =>
                    {
                        var duplicates = new List<char>();
                        for (var i = 0; i < 2; i++)
                        {
                            duplicates.AddRange(c.ElementAt(i).Items.FindDuplicates(c.ElementAt(i + 1).Items));
                        }

                        var duplicate = duplicates.GroupBy(c => c)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key)
                            .Distinct()
                            .Single();
                        var priority = new Priority(duplicate);
                        return priority;
                    }, cancellationToken: ct).ConfigureAwait(false);

                return priority;
            }
        }
    }
}