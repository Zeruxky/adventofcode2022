namespace AdventOfCode.Day8
{
    public record Tree
    {
        public int Height { get; init; }
        
        public MapCoordinate Coordinate { get; init; }
    }
}