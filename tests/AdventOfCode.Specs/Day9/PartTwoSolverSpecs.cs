using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day9;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day9
{
    [Trait("Category", "UnitTests")]
    public class PartTwoSolverSpecs : IAsyncDisposable
    {
        private readonly Stream stream;
        private readonly PartTwoSolver solver;

        public PartTwoSolverSpecs()
        {
            this.stream = new MemoryStream();
            this.solver = new PartTwoSolver();
        }

        [Fact]
        public async Task ShouldSolveExample()
        {
            // Arrange
            const int expectedVisitedPositions = 1;
            var lines = new[]
            {
                "R 4",
                "U 4",
                "L 3",
                "D 1",
                "R 4",
                "D 1",
                "L 5",
                "R 2",
            };
            await this.PrepareInputAsync(lines, CancellationToken.None).ConfigureAwait(false);
            
            // Act
            var visitedPositions = await this.solver
                .SolveAsync(this.stream, CancellationToken.None).
                ConfigureAwait(false);

            // Assert
            visitedPositions.Should().Be(expectedVisitedPositions);
        }

        [Fact]
        public async Task ShouldSolveLargerExample()
        {
            const int expectedVisitedPositions = 36;
            var lines = new[]
            {
                "R 5",
                "U 8",
                "L 8",
                "D 3",
                "R 17",
                "D 10",
                "L 25",
                "U 20",
            };
            await this.PrepareInputAsync(lines, CancellationToken.None).ConfigureAwait(false);
            
            // Act
            var visitedPositions = await this.solver
                .SolveAsync(this.stream, CancellationToken.None).
                ConfigureAwait(false);

            // Assert
            visitedPositions.Should().Be(expectedVisitedPositions);
        }
        
        public async ValueTask DisposeAsync()
        {
            await this.stream.DisposeAsync().ConfigureAwait(false);
        }

        private async Task PrepareInputAsync(IEnumerable<string> lines, CancellationToken ct)
        {
            await using (var writer = new StreamWriter(this.stream, Encoding.UTF8, leaveOpen: true))
            {
                foreach (var line in lines)
                {
                    await writer.WriteLineAsync(line.AsMemory(), ct).ConfigureAwait(false);
                }
            }

            this.stream.Seek(0, SeekOrigin.Begin);
        }
    }
}