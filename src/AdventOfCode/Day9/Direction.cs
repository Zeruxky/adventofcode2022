using Ardalis.SmartEnum;

namespace AdventOfCode.Day9
{
    public class Direction : SmartEnum<Direction>
    {
        public static readonly Direction Up = new Direction(nameof(Up), 0, 'U');
        public static readonly Direction Right = new Direction(nameof(Right), 1, 'R');
        public static readonly Direction Down = new Direction(nameof(Down), 2, 'D');
        public static readonly Direction Left = new Direction(nameof(Left), 3, 'L');

        private Direction(string name, int value, char shortcut)
            : base(name, value)
        {
            this.Shortcut = shortcut;
        }

        public char Shortcut { get; }

        public static bool TryParse(char value, out Direction? direction)
        {
            if (char.IsDigit(value))
            {
                direction = default;
                return false;
            }

            var selectedDirection = Direction.List.FirstOrDefault(d => d.Shortcut.Equals(value));
            if (selectedDirection is null)
            {
                direction = default;
                return false;
            }

            direction = selectedDirection;
            return true;
        }
    }
}