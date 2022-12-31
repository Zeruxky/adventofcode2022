using System.Buffers;

namespace AdventOfCode.Day5
{
    internal static class StackOfTExtensions
    {
        internal static void MoveTo<T>(this Stack<T> source, Stack<T> target, int items = 0)
        {
            for (var i = 0; i < items; i++)
            {
                var item = source.Pop();
                target.Push(item);
            }
        }

        internal static void MoveToAsPart<T>(this Stack<T> source, Stack<T> target, int items = 0)
        {
            var buffer = ArrayPool<T>.Shared.Rent(items);
            try
            {
                for (var i = 0; i < items; i++)
                {
                    var item = source.Pop();
                    buffer[i] = item;
                }

                foreach (var item in buffer.Reverse())
                {
                    if (item is null)
                    {
                        continue;
                    }
                    
                    target.Push(item);
                }
            }
            finally
            {
                ArrayPool<T>.Shared.Return(buffer, true);
            }
        }
    }
}