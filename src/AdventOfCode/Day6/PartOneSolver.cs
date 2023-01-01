namespace AdventOfCode.Day6
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Six;
        
        public Part Part => Part.One;
        
        public async Task<int> SolveAsync(Stream stream, CancellationToken ct)
        {
            using (var detector = new MarkerDetector(stream, true))
            {
                var position = await detector.DetectAsync(4, ct).ConfigureAwait(false);
                return position;
            }
        }
    }
}