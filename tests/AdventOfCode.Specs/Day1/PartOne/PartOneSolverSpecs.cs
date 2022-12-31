using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day1.PartOne;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AdventOfCode.Specs.Day1.PartOne
{
    [Trait("Category", "UnitTests")]
    public class PartOneSolverSpecs : IAsyncLifetime
    {
        private Stream input;
        private readonly PartOneSolver solver;

        public PartOneSolverSpecs()
        {
            this.input = new MemoryStream();
            this.solver = new PartOneSolver();
        }

        [Fact]
        public async Task ShouldSolveExample()
        {
            // Arrange
            const int expectedResult = 24000;

            // Act
            var result = await this.solver.SolveAsync(this.input, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task ShouldReturnDefaultValueIfFileIsEmpty()
        {
            // Arrange
            this.input = new MemoryStream();

            // Act
            var result = await this.solver.SolveAsync(this.input, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public async Task ShouldDetectWriteOnlyFile()
        {
            // Arrange
            this.input = Substitute.ForPartsOf<Stream>();
            this.input.Length.Returns(1);
            this.input.CanRead.Returns(false);

            // Act
            await this.solver
                .Awaiting(s => s.SolveAsync(this.input, CancellationToken.None))
                .Should()
                .ThrowExactlyAsync<ArgumentException>()
                .WithMessage("Can not read from write-only stream. (Parameter 'stream')")
                .ConfigureAwait(false);
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "1000",
                "2000",
                "3000",
                string.Empty,
                "4000",
                string.Empty,
                "5000",
                "6000",
                string.Empty,
                "7000",
                "8000",
                "9000",
                string.Empty,
                "10000",
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