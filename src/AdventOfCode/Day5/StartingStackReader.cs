using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day5
{
    public class StartingStackReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private long readCharacters;
        private bool disposed = false;

        public StartingStackReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<Stack<Crate>> ReadAllAsync([EnumeratorCancellation]CancellationToken ct)
        {
            if (this.reader.EndOfStream)
            {
                yield break;
            }

            var stacks = this.GetAllAsync(ct)
                .OrderBy(r => r.Position)
                .GroupBy(r => r.Position)
                .SelectAwait(async group =>
                {
                    var crates = await group.Select(g => (Crate)g.Value)
                        .Reverse()
                        .ToArrayAsync(ct)
                        .ConfigureAwait(false);
                
                    var stack = new Stack<Crate>(crates);
                    return stack;
                });

            await foreach (var stack in stacks.WithCancellation(ct).ConfigureAwait(false))
            {
                yield return stack;
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
        
        private async IAsyncEnumerable<ParsingResult> GetAllAsync([EnumeratorCancellation]CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    yield break;
                }
                
                this.readCharacters += line.Length;

                if (line == Environment.NewLine)
                {
                    yield break;
                }

                if (line == string.Empty)
                {
                    yield break;
                }

                if (line.Any(char.IsDigit))
                {
                    if (this.reader.BaseStream.CanSeek)
                    {
                        this.reader.BaseStream.Seek(readCharacters, SeekOrigin.Begin);
                    }
                    yield break;
                }

                var results = ParsingResult.Parse(line);
                foreach (var result in results)
                {
                    yield return result;
                }
            }
        }
    }
}