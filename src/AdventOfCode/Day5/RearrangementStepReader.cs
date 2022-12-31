using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day5
{
    public class RearrangementStepReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;

        public RearrangementStepReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<RearrangementStep> ReadAllAsync([EnumeratorCancellation]CancellationToken ct)
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

                if (!RearrangementStep.TryParse(line, out var step))
                {
                    continue;
                }

                yield return step!;
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