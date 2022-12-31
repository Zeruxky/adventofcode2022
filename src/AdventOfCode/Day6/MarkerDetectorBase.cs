using System.Text;

namespace AdventOfCode.Day6
{
    public abstract class MarkerDetectorBase : IDisposable
    {
        private readonly int packetSize;
        private readonly StreamReader reader;
        private readonly Encoding encoding = Encoding.UTF8;

        protected MarkerDetectorBase(int packetSize, Stream stream, bool leaveOpen)
        {
            this.packetSize = packetSize;
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        private bool disposed = false;

        public async Task<int> DetectAsync(CancellationToken ct)
        {
            var content = await reader.ReadToEndAsync().ConfigureAwait(false);
            using (var ringBuffer = new RingBuffer<char>(this.packetSize))
            {
                var position = 0;
                foreach (var character in content)
                {
                    ringBuffer.Add(character);
                    position++;

                    if (position <= this.packetSize)
                    {
                        continue;
                    }

                    var duplicates = ringBuffer.Where(char.IsLetter).FindDuplicates();
                    if (!duplicates.Any())
                    {
                        return position;
                    }
                }

                return -1;
            }
        }

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
                this.reader.Dispose();
            }

            this.disposed = true;
        }
    }
}