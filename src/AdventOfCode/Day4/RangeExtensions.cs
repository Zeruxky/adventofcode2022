namespace AdventOfCode.Day4
{
    public static class RangeExtensions
    {
        public static bool FullyContains(this Range current, Range other)
        {
            if (current.Start.Value <= other.Start.Value)
            {
                if (current.End.Value >= other.End.Value)
                {
                    return true;
                }
            }
            
            return false;
        }

        public static bool Overlaps(this Range current, Range other)
        {
            var first = current.GenerateSequence();
            var second = other.GenerateSequence();
            var overlaps = first.Intersect(second).Any();
            return overlaps;
        }

        public static IEnumerable<int> GenerateSequence(this Range range)
        {
            var start = range.Start.Value;
            var size = range.End.Value - range.Start.Value + 1;
            var sequence = Enumerable.Range(start, size);
            return sequence;
        }
    }
}