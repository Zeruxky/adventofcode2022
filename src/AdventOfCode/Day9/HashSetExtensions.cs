namespace AdventOfCode.Day9
{
    internal static class HashSetExtensions
    {
        internal static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                hashSet.Add(item);
            }
        }
    }
}