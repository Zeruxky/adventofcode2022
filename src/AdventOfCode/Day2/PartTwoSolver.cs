using System.Text;
using Ardalis.SmartEnum;

namespace AdventOfCode.Day2
{
    public class PartTwoSolver : ISolver<int>
    {
        public Day Day => Day.Two;
        
        public Part Part => Part.Two;
        
        public async Task<int> SolveAsync(Stream stream, CancellationToken ct)
        {
            if (stream.Length == 0)
            {
                return 0;
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read from write-only stream.", nameof(stream));
            }

            using (var reader = new UltraTopSecretStrategyGuideReader(stream, true))
            {
                var strategies = reader.GetAllAsync(ct);
                var totalScore = await strategies
                    .SumAsync(s => CalculateScore(s.Self, s.Opponent), ct)
                    .ConfigureAwait(false);

                return totalScore;
            }
        }
        
        private static int CalculateScore(Pick self, Pick opponent)
        {
            var score = 0;
            if (self == Pick.Rock)
            {
                if (opponent == Pick.Paper)
                {
                    score += 0;
                }

                if (opponent == Pick.Rock)
                {
                    score += 3;
                }

                if (opponent == Pick.Scissor)
                {
                    score += 6;
                }

                score += 1;
            }

            if (self == Pick.Paper)
            {
                if (opponent == Pick.Scissor)
                {
                    score += 0;
                }

                if (opponent == Pick.Paper)
                {
                    score += 3;
                }

                if (opponent == Pick.Rock)
                {
                    score += 6;
                }

                score += 2;
            }

            if (self == Pick.Scissor)
            {
                if (opponent == Pick.Rock)
                {
                    score += 0;
                }

                if (opponent == Pick.Scissor)
                {
                    score += 3;
                }

                if (opponent == Pick.Paper)
                {
                    score += 6;
                }

                score += 3;
            }

            return score;
        }
    }

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

    public class ExpectedOutcome : SmartEnum<ExpectedOutcome>
    {
        public static readonly ExpectedOutcome Loose = new ExpectedOutcome(nameof(Loose), 0);

        public static readonly ExpectedOutcome Draw = new ExpectedOutcome(nameof(Draw), 1);

        public static readonly ExpectedOutcome Win = new ExpectedOutcome(nameof(Win), 2);
        
        private ExpectedOutcome(string name, int value)
            : base(name, value)
        {
        }

        public static bool TryParse(char character, out ExpectedOutcome? outcome)
        {
            if (character == 'X')
            {
                outcome = ExpectedOutcome.Loose;
                return true;
            }

            if (character == 'Y')
            {
                outcome = Draw;
                return true;
            }

            if (character == 'Z')
            {
                outcome = Win;
                return true;
            }

            outcome = default;
            return false;
        }
    }
}