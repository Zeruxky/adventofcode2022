using Ardalis.SmartEnum;

namespace AdventOfCode.Day8
{
    public class Direction : SmartEnum<Direction>
    {
        public static readonly Direction North = new Direction(nameof(North), 0, 0, -1);
        public static readonly Direction East = new Direction(nameof(East), 1, 1, 0);
        public static readonly Direction South = new Direction(nameof(South), 2, 0, 1);
        public static readonly Direction West = new Direction(nameof(West), 3, 1, 0);
        
        private Direction(string name, int value, int x, int y)
            : base(name, value)
        {
            this.X = x;
            this.Y = y;
        }
        
        public int X { get; }
        
        public int Y { get; }
    }
}