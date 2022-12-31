namespace AdventOfCode.Day3
{
    public record Compartment
    {
        public Compartment(IEnumerable<char> items)
        {
            Items = items.ToArray();
        }

        public IReadOnlyCollection<char> Items { get; }
    }
}