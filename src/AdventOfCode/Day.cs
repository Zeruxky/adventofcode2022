using Ardalis.SmartEnum;

namespace AdventOfCode
{
    public class Day : SmartEnum<Day>
    {
        public static readonly Day One = new Day(nameof(One), 1);
        public static readonly Day Two = new Day(nameof(Two), 2);
        public static readonly Day Three = new Day(nameof(Three), 3);
        public static readonly Day Four = new Day(nameof(Four), 4);
        public static readonly Day Five = new Day(nameof(Five), 5);
        public static readonly Day Six = new Day(nameof(Six), 6);
        public static readonly Day Seven = new Day(nameof(Seven), 7);
        public static readonly Day Eight = new Day(nameof(Eight), 8);

        private Day(string name, int value)
            : base(name, value)
        {
        }
    }
}