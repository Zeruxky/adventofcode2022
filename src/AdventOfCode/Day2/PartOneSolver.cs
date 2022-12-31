namespace AdventOfCode.Day2
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Two;
        
        public Part Part => Part.One;
        
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

            using (var reader = new StrategyGuideReader(stream, true))
            {
                var strategies = reader.ReadAllAsync(ct);
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
}