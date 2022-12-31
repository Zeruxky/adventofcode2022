using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public record SectionAssignment
    {
        private static readonly Regex Regex = new Regex(@"^(?<start>\d+)\-(?<end>\d+)$");
        
        public SectionAssignment(int start, int end)
        {
            this.Range = new Range(start, end);
        }
        
        public Range Range { get; }

        public static bool TryParse(string value, out SectionAssignment? assignment)
        {
            var match = Regex.Match(value);
            if (!match.Success)
            {
                assignment = default;
                return false;
            }

            var startSpan = match.Groups["start"].ValueSpan;
            if (!int.TryParse(startSpan, out var start))
            {
                assignment = default;
                return false;
            }

            var endSpan = match.Groups["end"].ValueSpan;
            if (!int.TryParse(endSpan, out var end))
            {
                assignment = default;
                return false;
            }

            assignment = new SectionAssignment(start, end);
            return true;
        }
    }
}