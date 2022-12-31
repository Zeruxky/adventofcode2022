using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public record SectionAssignmentPair
    {
        private static readonly Regex Regex = new Regex(@"^(?<first>\d+\-\d+),(?<second>\d+\-\d+)$");
        
        public SectionAssignment First { get; init; }
        
        public SectionAssignment Second { get; init; }

        public static bool TryParse(string value, out SectionAssignmentPair? pair)
        {
            var match = Regex.Match(value);
            if (!match.Success)
            {
                pair = default;
                return false;
            }

            var firstSpan = match.Groups["first"].Value;
            if (!SectionAssignment.TryParse(firstSpan, out var first))
            {
                pair = default;
                return false;
            }

            var secondSpan = match.Groups["second"].Value;
            if (!SectionAssignment.TryParse(secondSpan, out var second))
            {
                pair = default;
                return false;
            }

            pair = new SectionAssignmentPair()
            {
                First = first!,
                Second = second!,
            };
            return true;
        }
    }
}