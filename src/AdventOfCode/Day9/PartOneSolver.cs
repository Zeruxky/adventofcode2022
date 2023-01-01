using System.Runtime.CompilerServices;

namespace AdventOfCode.Day9
{
    public class PartOneSolver : ISolver<int>
    {
        private MapCoordinate currentPositionHead;
        private MapCoordinate currentPositionTail;
        private readonly MapCoordinate startPosition;

        public PartOneSolver()
        {
            this.currentPositionHead = new MapCoordinate();
            this.currentPositionTail = new MapCoordinate();
            this.startPosition = new MapCoordinate();
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
                var visitedPositions = new HashSet<MapCoordinate>()
                {
                    this.startPosition,
                };

                var motions = reader.ReadAllAsync(ct);
                await foreach (var motion in motions.WithCancellation(ct).ConfigureAwait(false))
                {
                    visitedPositions.AddRange(this.GetVisitedPositions(motion));
                }
                
                return visitedPositions.Count;
            }
        }

        private IEnumerable<MapCoordinate> GetVisitedPositions(Motion motion)
        {
            for (var i = 0; i < motion.Steps; i++)
            {
                this.currentPositionHead = this.currentPositionHead.Move(motion.Direction);
                var offset = this.currentPositionHead - this.currentPositionTail;
                if (offset.X is -2 or 2)
                {
                    this.currentPositionTail = AdjustHorizontal(motion);
                    yield return this.currentPositionTail;
                }

                if (offset.Y is -2 or 2)
                {
                    this.currentPositionTail = AdjustVertical(motion);
                    yield return this.currentPositionTail;
                }
            }
        }

        private MapCoordinate AdjustVertical(Motion motion)
        {
            var offset = this.currentPositionHead - this.currentPositionTail;
            if (offset.Y is 2)
            {
                var movedTailPosition = this.MoveDown(motion, offset);
                return movedTailPosition;
            }

            if (offset.Y is -2)
            {
                var movedTailPosition = this.MoveUp(motion, offset);
                return movedTailPosition;
            }
            
            throw new InvalidOperationException($"Offset {offset} is out of range.");
        }

        private MapCoordinate MoveDown(Motion motion, MapCoordinate offset)
        {
            var movedTailPosition = this.currentPositionTail.Move(motion.Direction);
            if (offset.X == -1)
            {
                movedTailPosition = movedTailPosition.MoveLeft();
            }

            if (offset.X == 1)
            {
                movedTailPosition = movedTailPosition.MoveRight();
            }

            return movedTailPosition;
        }

        private MapCoordinate MoveUp(Motion motion, MapCoordinate offset)
        {
            var movedTailPosition = this.currentPositionTail.Move(motion.Direction);
            if (offset.X == -1)
            {
                movedTailPosition = movedTailPosition.MoveLeft();
            }

            if (offset.X == 1)
            {
                movedTailPosition = movedTailPosition.MoveRight();
            }

            return movedTailPosition;
        }

        private MapCoordinate AdjustHorizontal(Motion motion)
        {
            var offset = this.currentPositionHead - this.currentPositionTail;
            if (offset.X is 2)
            {
                var movedTailPosition = this.MoveRight(motion, offset);
                return movedTailPosition;
            }

            if (offset.X is -2)
            {
                return MoveLeft(motion, offset);
            }

            throw new InvalidOperationException($"Offset {offset} is out of range.");
        }

        private MapCoordinate MoveLeft(Motion motion, MapCoordinate offset)
        {
            var movedTailPosition = this.currentPositionTail.Move(motion.Direction);
            if (offset.Y == -1)
            {
                movedTailPosition = movedTailPosition.MoveUp();
            }

            if (offset.Y == 1)
            {
                movedTailPosition = movedTailPosition.MoveDown();
            }

            return movedTailPosition;
        }

        private MapCoordinate MoveRight(Motion motion, MapCoordinate offset)
        {
            var movedTailPosition = this.currentPositionTail.Move(motion.Direction);
            if (offset.Y == -1)
            {
                movedTailPosition = movedTailPosition.MoveUp();
            }

            if (offset.Y == 1)
            {
                movedTailPosition = movedTailPosition.MoveDown();
            }

            return movedTailPosition;
        }
    }
}