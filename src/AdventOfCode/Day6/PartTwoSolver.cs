namespace AdventOfCode.Day6
{
    public class PartTwoSolver : ISolver<int>
    {
        public Day Day => Day.Six;

        public Part Part => Part.Two;

        public async Task<int> SolveAsync(Stream stream, CancellationToken ct)
        {
            using (var detector = new MarkerDetector(stream, true))
            {
                var position = await detector.DetectAsync(14, ct).ConfigureAwait(false);
                return position;
            }
        }
    }
}