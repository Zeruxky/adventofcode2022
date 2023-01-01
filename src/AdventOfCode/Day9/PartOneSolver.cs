﻿namespace AdventOfCode.Day9
{
    public class PartOneSolver : ISolver<int>
    {
        private MapCoordinate headPosition;
        private MapCoordinate tailPosition;
        private readonly MapCoordinate startPosition;

        public PartOneSolver()
        {
            this.headPosition = new MapCoordinate();
            this.tailPosition = new MapCoordinate();
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
                this.headPosition = this.headPosition.Move(motion.Direction);
                var offset = this.headPosition - this.tailPosition;
                if (offset.X is -2 or 2)
                {
                    this.tailPosition = AdjustHorizontal(motion);
                    yield return this.tailPosition;
                }

                if (offset.Y is -2 or 2)
                {
                    this.tailPosition = AdjustVertical(motion);
                    yield return this.tailPosition;
                }
            }
        }

        private MapCoordinate AdjustVertical(Motion motion)
        {
            var offset = this.headPosition - this.tailPosition;
            if (offset.Y is 2)
            {
                var adjustedPosition = this.MoveDown(motion, offset);
                return adjustedPosition;
            }

            if (offset.Y is -2)
            {
                var adjustedPosition = this.MoveUp(motion, offset);
                return adjustedPosition;
            }
            
            throw new InvalidOperationException($"Offset {offset} is out of range.");
        }
        
        private MapCoordinate AdjustHorizontal(Motion motion)
        {
            var offset = this.headPosition - this.tailPosition;
            if (offset.X is 2)
            {
                var adjustedPosition = this.MoveRight(motion, offset);
                return adjustedPosition;
            }

            if (offset.X is -2)
            {
                var adjustedPosition = MoveLeft(motion, offset);
                return adjustedPosition;
            }

            throw new InvalidOperationException($"Offset {offset} is out of range.");
        }

        private MapCoordinate MoveDown(Motion motion, MapCoordinate offset)
        {
            var movedPosition = this.tailPosition.Move(motion.Direction);
            if (offset.X < 0)
            {
                movedPosition = movedPosition.MoveLeft();
            }

            if (offset.X > 0)
            {
                movedPosition = movedPosition.MoveRight();
            }

            return movedPosition;
        }

        private MapCoordinate MoveUp(Motion motion, MapCoordinate offset)
        {
            var movedPosition = this.tailPosition.Move(motion.Direction);
            if (offset.X < 0)
            {
                movedPosition = movedPosition.MoveLeft();
            }

            if (offset.X > 0)
            {
                movedPosition = movedPosition.MoveRight();
            }

            return movedPosition;
        }

        private MapCoordinate MoveLeft(Motion motion, MapCoordinate offset)
        {
            var movedPosition = this.tailPosition.Move(motion.Direction);
            if (offset.Y < 0)
            {
                movedPosition = movedPosition.MoveUp();
            }

            if (offset.Y > 0)
            {
                movedPosition = movedPosition.MoveDown();
            }

            return movedPosition;
        }

        private MapCoordinate MoveRight(Motion motion, MapCoordinate offset)
        {
            var movedPosition = this.tailPosition.Move(motion.Direction);
            if (offset.Y < 0)
            {
                movedPosition = movedPosition.MoveUp();
            }

            if (offset.Y > 0)
            {
                movedPosition = movedPosition.MoveDown();
            }

            return movedPosition;
        }
    }
}