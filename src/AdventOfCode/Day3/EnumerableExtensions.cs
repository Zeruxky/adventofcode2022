using System.Runtime.CompilerServices;

namespace AdventOfCode.Day3
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> FindDuplicates<T>(this IEnumerable<T> values, IEnumerable<T> otherValues)
        {
            var hashSet = new HashSet<T>(values);
            var duplicates = new HashSet<T>();
            foreach (var value in otherValues)
            {
                if (hashSet.Contains(value))
                {
                    duplicates.Add(value);
                }
            }
            
            return duplicates;
        }
        
        public static T FindDuplicate<T>(this IEnumerable<T> values, IEnumerable<T> otherValues)
        {
            var duplicate = values.FindDuplicates(otherValues).Distinct().Single();
            return duplicate;
        }

        public static async IAsyncEnumerable<IReadOnlyCollection<T>> ChunkAsync<T>(
            this IAsyncEnumerable<T> values,
            int chunkSize,
            [EnumeratorCancellation]CancellationToken ct)
        {
            var chunk = new List<T>(chunkSize);
            await foreach (var value in values.WithCancellation(ct).ConfigureAwait(false))
            {
                chunk.Add(value);
                
                if (chunk.Count == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                }
            }
        }
    }
}