namespace AdventOfCode.Day9
{
    public class PartTwoSolver : ISolver<int>
    {
        private readonly Rope rope;

        public PartTwoSolver()
        {
            this.rope = Rope.Create(10);
        }

        public Day Day => Day.Nine;
        
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
            
            using (var reader = new MotionReader(stream, true))
            {
                var visitedPositions = new HashSet<MapCoordinate>();
                var motions = reader.ReadAllAsync(ct);
                await foreach (var motion in motions.WithCancellation(ct).ConfigureAwait(false))
                {
                    for (var i = 0; i < motion.Steps; i++)
                    {
                        rope.Move(motion.Direction);
                        visitedPositions.Add(this.rope.Tail.Position);
                    }
                }
                
                return visitedPositions.Count;
            }
        }
    }
}