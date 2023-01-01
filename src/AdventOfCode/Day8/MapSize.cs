namespace AdventOfCode.Day8
{
    public record MapSize
    {
        public int Rows { get; init; }
        
        public int Columns { get; init; }

        public override string ToString()
        {
            return $"{this.Rows} X {this.Columns}";
        }
    }
}