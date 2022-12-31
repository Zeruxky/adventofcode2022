namespace AdventOfCode.Day5
{
    public class PartTwoSolver : ISolver<string>
    {
        public Day Day => Day.Five;
        
        public Part Part => Part.Two;
        
        public async Task<string> SolveAsync(Stream stream, CancellationToken ct)
        {
            if (stream.Length == 0)
            {
                return string.Empty;
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read from write-only stream.", nameof(stream));
            }

            using (var stackReader = new StartingStackReader(stream, true))
            {
                var stacks = await stackReader.ReadAllAsync(ct)
                    .ToArrayAsync(ct)
                    .ConfigureAwait(false);
                
                stackReader.Reset();

                using (var reader = new RearrangementStepReader(stream, true))
                {
                    await reader.ReadAllAsync(ct)
                        .ForEachAsync(s =>
                        {
                            var source = stacks[s.Source - 1];
                            var target = stacks[s.Target - 1];

                            source.MoveToAsPart(target, s.Crates);
                        }, cancellationToken: ct)
                        .ConfigureAwait(false);

                    var message = string.Join("", stacks.Select(s => (char)s.Peek()));
                    return message;
                }
            }
        }
    }
}