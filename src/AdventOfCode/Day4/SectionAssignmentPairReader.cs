using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day4
{
    public class SectionAssignmentPairReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;

        public SectionAssignmentPairReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<SectionAssignmentPair> ReadAllAsync([EnumeratorCancellation]CancellationToken ct)
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

                if (!SectionAssignmentPair.TryParse(line, out var pair))
                {
                    continue;
                }

                yield return pair!;
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