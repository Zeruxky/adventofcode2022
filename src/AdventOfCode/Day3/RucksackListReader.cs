using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day3
{
    public class RucksackListReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;
        
        public RucksackListReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<Rucksack> ReadAllAsync([EnumeratorCancellation] CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (this.reader.EndOfStream)
                {
                    yield break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    yield break;
                }

                var rucksack = new Rucksack(line);
                yield return rucksack;
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