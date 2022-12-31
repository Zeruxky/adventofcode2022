namespace AdventOfCode.Day1
{
    public readonly record struct CalorieItemInventory
    {
        private readonly List<CalorieItem> items;
    
        public CalorieItemInventory()
        {
            this.items = new List<CalorieItem>();
        }

        public IReadOnlyCollection<CalorieItem> Items => this.items;

        public int TotalCalories => this.Items.Sum(i => i);

        public void Add(CalorieItem item)
        {
            this.items.Add(item);
        }

        public override string ToString()
        {
            return $"Total items {this.items.Count}";
        }
    }
}