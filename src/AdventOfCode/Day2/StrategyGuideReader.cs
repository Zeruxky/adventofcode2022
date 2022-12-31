using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day2
{
    public class StrategyGuideReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;

        public StrategyGuideReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, encoding, leaveOpen: leaveOpen);
        }
        
        public async IAsyncEnumerable<Strategy> ReadAllAsync([EnumeratorCancellation] CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (reader.EndOfStream)
                {
                    yield break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    yield break;
                }

                if (Strategy.TryParse(line, out var strategy))
                {
                    yield return strategy;
                }
            }
            
            ct.ThrowIfCancellationRequested();
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