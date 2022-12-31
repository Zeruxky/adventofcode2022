using Ardalis.SmartEnum;

namespace AdventOfCode.Day2
{
    public class Pick : SmartEnum<Pick>
    {
        public static readonly Pick Rock = new Pick(nameof(Rock), 0);
        public static readonly Pick Paper = new Pick(nameof(Paper), 1);
        public static readonly Pick Scissor = new Pick(nameof(Scissor), 2);

        private Pick(string name, int value)
            : base(name, value)
        {
        }

        public static bool TryParse(char value, out Pick? pick)
        {
            value = char.ToUpper(value);
            switch (value)
            {
                case 'A':
                    pick = Rock;
                    return true;
                case 'B':
                    pick = Paper;
                    return true;
                case 'C':
                    pick = Scissor;
                    return true;
                case 'X':
                    pick = Rock;
                    return true;
                case 'Y':
                    pick = Paper;
                    return true;
                case 'Z':
                    pick = Scissor;
                    return true;
                default:
                    pick = default;
                    return false;
            }
        }
    }
}