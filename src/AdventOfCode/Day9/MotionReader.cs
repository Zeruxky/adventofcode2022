using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day9
{
    public class MotionReader : IDisposable
    {
        private static readonly Encoding Encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;

        public MotionReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, Encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<Motion> ReadAllAsync([EnumeratorCancellation] CancellationToken ct)
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

                if (Motion.TryParse(line, out var motion))
                {
                    yield return motion!;
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