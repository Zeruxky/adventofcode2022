using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day8;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day8
{
    [Trait("Category", "UnitTests")]
    public class PartTwoSolverSpecs : IAsyncLifetime
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
            const int expectedScenicScore = 8;

            // Act
            var scenicScore = await this.solver.SolveAsync(this.stream, CancellationToken.None).ConfigureAwait(false);

            // Assert
            scenicScore.Should().Be(expectedScenicScore);
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "30373",
                "25512",
                "65332",
                "33549",
                "35390",
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