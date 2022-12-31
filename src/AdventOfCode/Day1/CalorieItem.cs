namespace AdventOfCode.Day1
{
    public readonly record struct CalorieItem
    {
        private readonly int value;

        public CalorieItem(int value)
        {
            this.value = value;
        }

        public static explicit operator CalorieItem(int value) => new CalorieItem(value);

        public static implicit operator int(CalorieItem item) => item.value;

        public static bool TryParse(string value, out CalorieItem item)
        {
            return CalorieItem.TryParse(value.AsSpan(), out item);
        }

        public static bool TryParse(ReadOnlySpan<char> value, out CalorieItem item)
        {
            if (int.TryParse(value, out var calories))
            {
                item = new CalorieItem(calories);
                return true;
            }

            item = default;
            return false;
        }

        public override string ToString()
        {
            return $"{this.value} Calories";
        }
    }
}