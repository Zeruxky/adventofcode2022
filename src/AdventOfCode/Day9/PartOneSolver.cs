namespace AdventOfCode.Day9
{
    public class PartOneSolver : ISolver<int>
    {
        private readonly Rope rope;

        public PartOneSolver()
        {
            this.rope = Rope.Create(2);
        }

        public Day Day => Day.Nine;

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
            
            using (var reader = new MotionReader(stream, true))
            {
                var visitedPositions = new HashSet<MapCoordinate>();
                var motions = reader.ReadAllAsync(ct);
                await foreach (var motion in motions.WithCancellation(ct).ConfigureAwait(false))
                {
                    for (var i = 0; i < motion.Steps; i++)
                    {
                        this.rope.Move(motion.Direction);
                        visitedPositions.Add(this.rope.Tail.Position);
                    }
                }
                
                return visitedPositions.Count;
            }
        }
    }
}