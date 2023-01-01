using System;
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
    public class PartOneSolverSpecs : IAsyncLifetime
    {
        private readonly Stream stream;
        private readonly PartOneSolver solver;

        public PartOneSolverSpecs()
        {
            this.stream = new MemoryStream();
            this.solver = new PartOneSolver();
        }

        [Fact]
        public async Task ShouldSolveExample()
        {
            // Arrange
            const int expectedVisitedPositions = 13;
            
            // Act
            var visitedPositions = await this.solver
                .SolveAsync(this.stream, CancellationToken.None).
                ConfigureAwait(false);

            // Assert
            visitedPositions.Should().Be(expectedVisitedPositions);
        }

        public async Task InitializeAsync()
        {
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

            await using (var writer = new StreamWriter(this.stream, Encoding.UTF8, leaveOpen: true))
            {
                foreach (var line in lines)
                {
                    await writer.WriteLineAsync(line.AsMemory(), CancellationToken.None).ConfigureAwait(false);
                }
            }

            this.stream.Seek(0, SeekOrigin.Begin);
        }

        public async Task DisposeAsync()
        {
            await this.stream.DisposeAsync().ConfigureAwait(false);
        }
    }
}