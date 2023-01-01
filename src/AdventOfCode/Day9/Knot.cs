namespace AdventOfCode.Day9
{
    public record Knot
    {
        public int Index { get; init; }

        public MapCoordinate Position { get; private set; } = new MapCoordinate();

        public void Move(Direction direction)
        {
            if (direction == Direction.Up)
            {
                this.Position = this.Position with { Y = this.Position.Y - 1};
            }

            if (direction == Direction.Down)
            {
                this.Position = this.Position with { Y = this.Position.Y + 1};
            }
            
            if (direction == Direction.Left)
            {
                this.Position = this.Position with { X = this.Position.X - 1};
            }
            
            if (direction == Direction.Right)
            {
                this.Position = this.Position with { X = this.Position.X + 1};
            }
        }

        public override string ToString()
        {
            return $"{this.Index}: {this.Position}";
        }
    }
}