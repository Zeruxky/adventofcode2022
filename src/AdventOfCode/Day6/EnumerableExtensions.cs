namespace AdventOfCode.Day6
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> FindDuplicates<T>(this IEnumerable<T> values)
        {
            var hashSet = new HashSet<T>();
            foreach (var value in values)
            {
                if (!hashSet.Add(value))
                {
                    yield return value;
                }
            }
        }
    }
}