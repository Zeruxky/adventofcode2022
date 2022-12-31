namespace AdventOfCode.Day3
{
    public record Rucksack
    {
        public Rucksack(string itemList)
        {
            if (itemList.Length % 2 != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(itemList), "Item list can not be divided by 2");
            }

            this.Items = itemList.ToArray();
            var compartmentSize = itemList.Length / 2;
            this.Left = new Compartment(itemList.AsSpan(0, compartmentSize).ToArray());
            this.Right = new Compartment(itemList.AsSpan(compartmentSize).ToArray());
        }

        public IReadOnlyCollection<char> Items { get; }

        public Compartment Left { get; }
        
        public Compartment Right { get; }
    }
}