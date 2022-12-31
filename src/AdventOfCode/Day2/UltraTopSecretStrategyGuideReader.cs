using System.Text;

namespace AdventOfCode.Day2
{
    public class UltraTopSecretStrategyGuideReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;
        
        public UltraTopSecretStrategyGuideReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, encoding, leaveOpen);
        }
        
        public async IAsyncEnumerable<Strategy> GetAllAsync(CancellationToken ct)
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

                if (!Pick.TryParse(line[0], out var opponent))
                {
                    continue;
                }

                var index = line.LastIndexOf(' ');
                if (!ExpectedOutcome.TryParse(line[index + 1], out var expectedOutcome))
                {
                    continue;
                }

                if (expectedOutcome == ExpectedOutcome.Loose)
                {
                    if (opponent == Pick.Paper)
                    {
                        var strategy = new Strategy(opponent, Pick.Rock);
                        yield return strategy;
                    }

                    if (opponent == Pick.Rock)
                    {
                        var strategy = new Strategy(opponent, Pick.Scissor);
                        yield return strategy;
                    }

                    if (opponent == Pick.Scissor)
                    {
                        var strategy = new Strategy(opponent, Pick.Paper);
                        yield return strategy;
                    }
                }
                    
                if (expectedOutcome == ExpectedOutcome.Draw)
                {
                    if (opponent == Pick.Paper)
                    {
                        var strategy = new Strategy(opponent, opponent);
                        yield return strategy;
                    }

                    if (opponent == Pick.Rock)
                    {
                        var strategy = new Strategy(opponent, opponent);
                        yield return strategy;
                    }

                    if (opponent == Pick.Scissor)
                    {
                        var strategy = new Strategy(opponent, opponent);
                        yield return strategy;
                    }
                }
                    
                if (expectedOutcome == ExpectedOutcome.Win)
                {
                    if (opponent == Pick.Paper)
                    {
                        var strategy = new Strategy(opponent, Pick.Scissor);
                        yield return strategy;
                    }

                    if (opponent == Pick.Rock)
                    {
                        var strategy = new Strategy(opponent, Pick.Paper);
                        yield return strategy;
                    }

                    if (opponent == Pick.Scissor)
                    {
                        var strategy = new Strategy(opponent, Pick.Rock);
                        yield return strategy;
                    }
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
                reader.Dispose();
            }

            this.disposed = true;
        }
    }
}