using Ardalis.SmartEnum;

namespace AdventOfCode.Day8
{
    public class Direction : SmartEnum<Direction>
    {
        public static readonly Direction North = new Direction(nameof(North), 0);
        public static readonly Direction East = new Direction(nameof(East), 1);
        public static readonly Direction South = new Direction(nameof(South), 2);
        public static readonly Direction West = new Direction(nameof(West), 3);
        
        private Direction(string name, int value)
            : base(name, value)
        {
        }
    }
}