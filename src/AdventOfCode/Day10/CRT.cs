using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;

namespace AdventOfCode.Day10
{
    public record CRT : IAsyncDisposable
    {
        private readonly ChannelReader<int> spritePositions;
        private readonly Sprite sprite;
        private readonly Stream display;
        private readonly StreamWriter displayWriter;
        private int position;
        private bool disposed = false;

        public CRT(ChannelReader<int> spritePositions)
        {
            this.spritePositions = spritePositions;
            this.sprite = new Sprite();
            this.display = new MemoryStream();
            this.displayWriter = new StreamWriter(this.display, Encoding.UTF8);
        }

        public async Task RunAsync(CancellationToken ct)
        {
            await foreach (var spritePosition in this.spritePositions.ReadAllAsync(ct).ConfigureAwait(false))
            {
                await this.DrawAsync(spritePosition, ct).ConfigureAwait(false);
            }
        }

        private async Task DrawAsync(int spritePosition, CancellationToken ct)
        {
            sprite.MoveTo(spritePosition);

            var characterToWrite = sprite.Pixels.Contains(position)
                ? "#"
                : ".";

            await this.displayWriter.WriteAsync(characterToWrite).ConfigureAwait(false);
            this.position++;
            if (this.position == 40)
            {
                await this.displayWriter.WriteAsync(Environment.NewLine.AsMemory(), ct).ConfigureAwait(false);
                await this.displayWriter.FlushAsync().ConfigureAwait(false);
                this.position = 0;
            }
        }

        public async IAsyncEnumerable<string> PrintAsync([EnumeratorCancellation] CancellationToken ct)
        {
            this.display.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(this.display, Encoding.UTF8, leaveOpen: true))
            {
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    if (reader.EndOfStream)
                    {
                        yield break;
                    }

                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (line is null)
                    {
                        yield break;
                    }

                    yield return line;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsync(true).ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                await this.displayWriter.DisposeAsync().ConfigureAwait(false);
            }

            this.disposed = true;
        }
    }
}