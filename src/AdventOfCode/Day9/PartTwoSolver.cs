namespace AdventOfCode.Day9
{
    public class PartTwoSolver : ISolver<int>
    {
        private readonly MapCoordinate startPosition;
        private MapCoordinate headPosition;
        private readonly MapCoordinate[] knotPositions;

        public PartTwoSolver()
        {
            this.startPosition = new MapCoordinate();
            this.headPosition = new MapCoordinate();
            this.knotPositions = Enumerable.Range(0, 9).Select(i => new MapCoordinate()).ToArray();
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
                var visitedPositions = new HashSet<MapCoordinate>()
                {
                    this.startPosition,
                };

                var motions = reader.ReadAllAsync(ct);
                await foreach (var motion in motions.WithCancellation(ct).ConfigureAwait(false))
                {
                    for (var i = 0; i < motion.Steps; i++)
                    {
                        this.headPosition = this.headPosition.Move(motion.Direction);
                        var toFollow = this.headPosition;
                        for (var j = 0; j < this.knotPositions.Length; j++)
                        {
                            this.knotPositions[j] = this.Follow(toFollow, this.knotPositions[j]);
                            toFollow = knotPositions[j];
                        }

                        var tail = this.knotPositions.Last();
                        visitedPositions.Add(tail);
                    }
                }
                
                return visitedPositions.Count;
            }
        }
        
        private MapCoordinate Follow(MapCoordinate toFollow, MapCoordinate follower)
        {
            var offset = toFollow - follower;
            if (offset.X is -2 or 2)
            {
                return AdjustHorizontal(toFollow, follower);
            }

            if (offset.Y is -2 or 2)
            {
                return AdjustVertical(toFollow, follower);
            }

            return follower;
        }

        private MapCoordinate AdjustVertical(MapCoordinate toFollow, MapCoordinate follower)
        {
            var offset = toFollow - follower;
            if (offset.Y == 2)
            {
                var adjustedPosition = this.MoveDown(follower, offset);
                return adjustedPosition;
            }

            if (offset.Y == -2)
            {
                var adjustedPosition = this.MoveUp(follower, offset);
                return adjustedPosition;
            }
            
            throw new InvalidOperationException($"Offset {offset} is out of range.");
        }
        
        private MapCoordinate AdjustHorizontal(MapCoordinate toFollow, MapCoordinate follower)
        {
            var offset = toFollow - follower;
            if (offset.X == 2)
            {
                var adjustedPosition = this.MoveRight(follower, offset);
                return adjustedPosition;
            }

            if (offset.X == -2)
            {
                var adjustedPosition = MoveLeft(follower, offset);
                return adjustedPosition;
            }

            throw new InvalidOperationException($"Offset {offset} is out of range.");
        }

        private MapCoordinate MoveDown(MapCoordinate follower, MapCoordinate offset)
        {
            var movedPosition = follower.MoveDown();
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

        private MapCoordinate MoveUp(MapCoordinate follower, MapCoordinate offset)
        {
            var movedPosition = follower.MoveUp();
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

        private MapCoordinate MoveLeft(MapCoordinate follower, MapCoordinate offset)
        {
            var movedPosition = follower.MoveLeft();
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

        private MapCoordinate MoveRight(MapCoordinate follower, MapCoordinate offset)
        {
            var movedPosition = follower.MoveRight();
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