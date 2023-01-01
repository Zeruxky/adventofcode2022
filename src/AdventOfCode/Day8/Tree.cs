namespace AdventOfCode.Day8
{
    public record Tree
    {
        public int Height { get; init; }
        
        public MapCoordinate Coordinate { get; init; }

        public bool IsOnEdge(MapSize size)
        {
            if (this.Coordinate.X == 0)
            {
                return true;
            }
            
            if (this.Coordinate.Y == 0)
            {
                return true;
            }

            if (this.Coordinate.X == size.Columns)
            {
                return true;
            }

            if (this.Coordinate.Y == size.Rows)
            {
                return true;
            }

            return false;
        }
    }
}