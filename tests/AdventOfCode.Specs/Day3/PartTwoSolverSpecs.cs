using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day3.PartTwo;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day3
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
            const int expectedSum = 70;

            // Act
            var sum = await this.solver.SolveAsync(this.stream, CancellationToken.None).ConfigureAwait(false);

            // Assert
            sum.Should().Be(expectedSum);
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "vJrwpWtwJgWrhcsFMMfFFhFp",
                "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
                "PmmdzqPrVvPwwTWBwg",
                "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
                "ttgJtRGJQctTZtZT",
                "CrZsJsPPZsGzwwsLwLmpwMDw",
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