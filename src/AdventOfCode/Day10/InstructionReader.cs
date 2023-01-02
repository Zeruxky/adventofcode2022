using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day10
{
    public class InstructionReader : IDisposable
    {
        private static readonly Encoding UsedEncoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;

        public InstructionReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, UsedEncoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<Instruction> ReadAllAsync([EnumeratorCancellation] CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                if (this.reader.EndOfStream)
                {
                    yield break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    yield break;
                }

                if (Instruction.TryParse(line, out var instruction))
                {
                    yield return instruction!;
                }
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