namespace AdventOfCode.Day1.PartOne
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.One;

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

            using (var reader = new InventoryListReader(stream, true))
            {
                var inventories = reader.ReadAllAsync(ct);
                var totalCalories = await inventories
                    .MaxAsync(i => i.TotalCalories, cancellationToken: ct)
                    .ConfigureAwait(false);

                return totalCalories;
            }
        }
    }
}