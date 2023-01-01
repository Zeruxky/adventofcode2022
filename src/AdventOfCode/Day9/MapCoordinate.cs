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

        public MapCoordinate Move(Direction direction)
        {
            if (direction == Direction.Up)
            {
                return this.MoveUp();
            }

            if (direction == Direction.Down)
            {
                return this.MoveDown();
            }

            if (direction == Direction.Left)
            {
                return this.MoveLeft();
            }

            if (direction == Direction.Right)
            {
                return this.MoveRight();
            }

            throw new ArgumentException($"Unknown direction {direction}.", nameof(direction));
        }

        public MapCoordinate MoveLeft()
        {
            var coordinate = this with { X = this.X - 1 };
            return coordinate;
        }

        public MapCoordinate MoveRight()
        {
            var coordinate = this with { X = this.X + 1 };
            return coordinate;
        }

        public MapCoordinate MoveUp()
        {
            var coordinate = this with { Y = this.Y - 1 };
            return coordinate;
        }

        public MapCoordinate MoveDown()
        {
            var coordinate = this with { Y = this.Y + 1 };
            return coordinate;
        }
    }
}