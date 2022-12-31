using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day8
{
    public class TreeMapReader : IDisposable
    {
        private readonly StreamReader reader;
        private readonly Encoding encoding = Encoding.UTF8;
        private bool disposed = false;

        public TreeMapReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<Tree> ReadAllAsync([EnumeratorCancellation] CancellationToken ct)
        {
            var positionY = 1;
            while (!ct.IsCancellationRequested)
            {
                var positionX = 1;
                
                if (this.reader.EndOfStream)
                {
                    yield break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    yield break;
                }

                foreach (var size in line.Select(character => int.Parse(character.ToString())))
                {
                    yield return new Tree()
                    {
                        Height = size,
                        Coordinate = new MapCoordinate()
                        {
                            X = positionX++,
                            Y = positionY,
                        },
                    };
                }
                
                positionY++;
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