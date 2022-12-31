namespace AdventOfCode.Day3.PartOne
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Three;

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
            
            using (var reader = new RucksackListReader(stream, true))
            {
                var rucksacks = reader.ReadAllAsync(ct);
                var sum = await rucksacks
                    .SumAsync(r =>
                    {
                        var duplicate = r.Left.Items.FindDuplicate(r.Right.Items);
                        var priority = new Priority(duplicate);
                        return priority;
                    }, cancellationToken: ct).ConfigureAwait(false);

                return sum;
            }
        }
    }
}