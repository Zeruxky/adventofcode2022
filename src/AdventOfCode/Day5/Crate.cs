namespace AdventOfCode.Day5
{
    public record Crate
    {
        private readonly char value;
        
        private Crate(char value)
        {
            this.value = value;
        }

        public static explicit operator Crate(char value) => new Crate(value);

        public static implicit operator char(Crate crate) => crate.value;

        public override string ToString()
        {
            return $"[{value}]";
        }
    }
}