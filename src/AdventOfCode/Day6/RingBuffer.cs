using System.Buffers;
using System.Collections;

namespace AdventOfCode.Day6
{
    public class RingBuffer<T> : IDisposable, IEnumerable<T>
    {
        private readonly T[] items;
        private readonly int size;
        private int position;
        private bool disposed = false;

        public RingBuffer(int size)
        {
            this.position = 0;
            this.size = size;
            this.items = ArrayPool<T>.Shared.Rent(size);
        }

        public void Add(T item)
        {
            this.items[this.position % this.size] = item;
            this.position++;
            if (this.position == this.size)
            {
                this.position = 0;
            }
        }

        public T this[int index] => this.items[index % this.size];

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                ArrayPool<T>.Shared.Return(this.items, true);
            }

            this.disposed = true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.items.Where(i => i is not null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}