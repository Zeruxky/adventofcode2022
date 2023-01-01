namespace AdventOfCode.Day9
{
    public record MapCoordinate
    {
        public int X { get; init; } = 0;

        public int Y { get; init; } = 0;

        public static MapCoordinate operator -(MapCoordinate left, MapCoordinate right)
        {
            var coordinate = new MapCoordinate()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
            };

            return coordinate;
        }
        
        public IEnumerable<Direction> CalculateRoute(MapCoordinate target)
        {
            var offset = target - this;
            if (Math.Abs(offset.X) == 2 || Math.Abs(offset.Y) == 2)
            {
                if (offset.X > 0)
                {
                    yield return Direction.Right;
                }
                
                if (offset.X < 0)
                {
                    yield return Direction.Left;
                }
                
                if (offset.Y > 0)
                {
                    yield return Direction.Down;
                }

                if (offset.Y < 0)
                {
                    yield return Direction.Up;
                }
            }
        }
    }
}