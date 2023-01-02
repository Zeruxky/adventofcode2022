namespace AdventOfCode.Day10
{
    public record Register
    {
        public char Id { get; init; }

        public int Value { get; private set; } = 1;

        public void Add(int value)
        {
            this.Value += value;
        }
    }
}