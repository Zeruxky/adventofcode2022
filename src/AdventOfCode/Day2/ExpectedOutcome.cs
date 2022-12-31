using Ardalis.SmartEnum;

namespace AdventOfCode.Day2
{
    public class ExpectedOutcome : SmartEnum<ExpectedOutcome>
    {
        public static readonly ExpectedOutcome Loose = new ExpectedOutcome(nameof(Loose), 0);

        public static readonly ExpectedOutcome Draw = new ExpectedOutcome(nameof(Draw), 1);

        public static readonly ExpectedOutcome Win = new ExpectedOutcome(nameof(Win), 2);
        
        private ExpectedOutcome(string name, int value)
            : base(name, value)
        {
        }

        public static bool TryParse(char character, out ExpectedOutcome? outcome)
        {
            if (character == 'X')
            {
                outcome = ExpectedOutcome.Loose;
                return true;
            }

            if (character == 'Y')
            {
                outcome = Draw;
                return true;
            }

            if (character == 'Z')
            {
                outcome = Win;
                return true;
            }

            outcome = default;
            return false;
        }
    }
}