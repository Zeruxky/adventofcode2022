using Ardalis.SmartEnum;

namespace AdventOfCode
{
    public class Part : SmartEnum<Part>
    {
        public static readonly Part One = new Part(nameof(One), 1);

        public static readonly Part Two = new Part(nameof(Two), 2);

        public static readonly Part All = new Part(nameof(All), 3);
    
        private Part(string name, int value)
            : base(name, value)
        {
        }
    }
}