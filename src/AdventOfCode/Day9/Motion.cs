namespace AdventOfCode.Day9
{
    public record Motion
    {
        public int Steps { get; init; }

        public Direction Direction { get; init; }

        public static bool TryParse(string value, out Motion? motion)
        {
            if (string.IsNullOrEmpty(value))
            {
                motion = default;
                return false;
            }

            if (!Day9.Direction.TryParse(value[0], out var direction))
            {
                motion = default;
                return false;
            }

            var indexOfWhitespace = value.IndexOf(' ');
            if (!int.TryParse(value.AsSpan(indexOfWhitespace + 1), out var steps))
            {
                motion = default;
                return false;
            }

            motion = new Motion()
            {
                Direction = direction!,
                Steps = steps,
            };
            return true;
        }

        public override string ToString()
        {
            return $"{this.Direction.Shortcut} {this.Steps}";
        }
    }
}