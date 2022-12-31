namespace AdventOfCode.Day3
{
    public record Priority
    {
        private static readonly Dictionary<char, int> Lookup = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
            .Zip(Enumerable.Range(1, 56))
            .ToDictionary(c => c.First, c=> c.Second);

        private readonly int value;
        
        public Priority(char value)
        {
            if (!Lookup.TryGetValue(value, out var priority))
            {
                this.value = 0;
            }
            
            this.value = priority;
        }

        public static explicit operator Priority(char value) => new Priority(value);

        public static implicit operator int(Priority priority) => priority.value;
    }
}