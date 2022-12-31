using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day2;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day2
{
    [Trait("Category", "UnitTests")]
    public class PartTwoSolverSpecs : IAsyncLifetime
    {
        private readonly Stream input;
        private readonly PartTwoSolver solver;

        public PartTwoSolverSpecs()
        {
            this.input = new MemoryStream();
            this.solver = new PartTwoSolver();
        }
        
        [Fact]
        public async Task ShouldSolveExample()
        {
            // Arrange
            const int expectedScore = 12;

            // Act
            var score = await this.solver.SolveAsync(this.input, CancellationToken.None).ConfigureAwait(false);

            // Assert
            expectedScore.Should().Be(score);
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "A Y",
                "B X",
                "C Z",
            };

            await using (var writer = new StreamWriter(this.input, Encoding.UTF8, leaveOpen: true))
            {
                foreach (var line in lines)
                {
                    await writer.WriteLineAsync(line.AsMemory(), CancellationToken.None).ConfigureAwait(false);
                }
            }

            this.input.Seek(0, SeekOrigin.Begin);
        }

        public async Task DisposeAsync()
        {
            await this.input.DisposeAsync().ConfigureAwait(false);
        }
    }
}