namespace AdventOfCode.Day10
{
    public record Sprite
    {
        private int value;

        public Sprite()
        {
            this.value = 0;
        }

        public IEnumerable<int> Pixels => this.value == 1
            ? Enumerable.Range(0, 3)
            : Enumerable.Range(value - 1, 3);

        public void MoveTo(int position)
        {
            this.value = position;
        }
    }
}