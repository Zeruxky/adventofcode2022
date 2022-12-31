using System.Text.RegularExpressions;

namespace AdventOfCode.Day5
{
    public record RearrangementStep
    {
        private static readonly Regex Regex = new Regex(@"^move (?<crates>\d+) from (?<source>\d+) to (?<target>\d+)$");
        
        public int Source { get; init; }
        
        public int Target { get; init; }
        
        public int Crates { get; init; }

        public override string ToString()
        {
            return $"Move {this.Crates} from {this.Source} to {this.Target}.";
        }

        public static bool TryParse(string value, out RearrangementStep? step)
        {
            var match = Regex.Match(value);
            if (!match.Success)
            {
                step = default;
                return false;
            }

            if (!int.TryParse(match.Groups["crates"].ValueSpan, out var crates))
            {
                step = default;
                return false;
            }
            
            if (!int.TryParse(match.Groups["source"].ValueSpan, out var source))
            {
                step = default;
                return false;
            }
            
            if (!int.TryParse(match.Groups["target"].ValueSpan, out var target))
            {
                step = default;
                return false;
            }

            step = new RearrangementStep()
            {
                Crates = crates,
                Source = source,
                Target = target,
            };
            return true;
        }
    }
}