using System.Runtime.CompilerServices;

namespace AdventOfCode.Day9
{
    public class PartOneSolver : ISolver<int>
    {
        private MapCoordinate currentPositionHead;
        private MapCoordinate currentPositionTail;

        public PartOneSolver()
        {
            this.currentPositionHead = new MapCoordinate()
            {
                X = 0,
                Y = 0,
            };
            this.currentPositionTail = new MapCoordinate()
            {
                X = 0,
                Y = 0,
            };
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
                var motions = reader.ReadAllAsync(ct);
                var visitedPositions = await this.GetVisitedPositionsAsync(motions, ct)
                    .Distinct()
                    .CountAsync(ct)
                    .ConfigureAwait(false);
                
                return visitedPositions;
            }
        }

        private async IAsyncEnumerable<MapCoordinate> GetVisitedPositionsAsync(
            IAsyncEnumerable<Motion> motions,
            [EnumeratorCancellation] CancellationToken ct)
        {

            yield return this.currentPositionTail;
            
            await foreach (var motion in motions.WithCancellation(ct).ConfigureAwait(false))
            {
                for (var i = 0; i < motion.Steps; i++)
                {
                    this.currentPositionHead = this.currentPositionHead.Move(motion.Direction);
                    var offset = this.currentPositionHead - this.currentPositionTail;
                    if (offset.X is 2)
                    {
                        if (offset.Y == -1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveRight()
                                .MoveUp();
                            
                            yield return this.currentPositionTail;
                        }
                        
                        if (offset.Y == 0)
                        {
                            this.currentPositionTail = this.currentPositionTail.Move(motion.Direction);
                            yield return this.currentPositionTail;
                        }
                        
                        if (offset.Y == 1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveRight()
                                .MoveDown();

                            yield return this.currentPositionTail;
                        }
                        
                        continue;
                    }
                    
                    if (offset.X is -2)
                    {
                        if (offset.Y == 0)
                        {
                            this.currentPositionTail = this.currentPositionTail.Move(motion.Direction);
                            yield return this.currentPositionTail;
                        }
                        
                        if (offset.Y == 1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveLeft()
                                .MoveDown();

                            yield return this.currentPositionTail;
                        }

                        if (offset.Y == -1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveLeft()
                                .MoveUp();
                            
                            yield return this.currentPositionTail;
                        }
                        
                        continue;
                    }
                    
                    if (offset.Y is 2)
                    {
                        if (offset.X == 0)
                        {
                            this.currentPositionTail = this.currentPositionTail.Move(motion.Direction);
                            yield return this.currentPositionTail;
                        }

                        if (offset.X == 1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveRight()
                                .MoveDown();
                            
                            yield return this.currentPositionTail;
                        }

                        if (offset.X == -1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveLeft()
                                .MoveDown();
                            
                            yield return this.currentPositionTail;
                        }
                        
                        continue;
                    }
                    
                    if (offset.Y is -2)
                    {
                        if (offset.X == 0)
                        {
                            this.currentPositionTail = this.currentPositionTail.Move(motion.Direction);
                            yield return this.currentPositionTail;
                        }

                        if (offset.X == 1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveRight()
                                .MoveUp();
                            
                            yield return this.currentPositionTail;
                        }

                        if (offset.X == -1)
                        {
                            this.currentPositionTail = this.currentPositionTail
                                .MoveLeft()
                                .MoveUp();
                            
                            yield return this.currentPositionTail;
                        }
                    }
                }
            }
        }
    }
}