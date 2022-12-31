using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day2;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AdventOfCode.Specs.Day2
{
    [Trait("Category", "UnitTests")]
    public class PartOneSolverSpecs : IAsyncLifetime
    {
        private Stream stream;
        private readonly PartOneSolver solver;

        public PartOneSolverSpecs()
        {
            this.stream = new MemoryStream();
            this.solver = new PartOneSolver();
        }

        [Fact]
        public async Task ShouldReturnDefaultOnEmptyInput()
        {
            // Arrange
            this.stream = new MemoryStream();

            // Act
            var result = await this.solver.SolveAsync(this.stream, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public async Task ShouldRefuseWriteOnlyStream()
        {
            // Arrange
            this.stream = Substitute.ForPartsOf<Stream>();
            this.stream.Length.Returns(1);
            this.stream.CanRead.Returns(false);

            // Act
            await this.solver
                .Awaiting(s => s.SolveAsync(this.stream, CancellationToken.None))
                .Should()
                .ThrowExactlyAsync<ArgumentException>()
                .WithMessage("Can not read from write-only stream. (Parameter 'stream')")
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task ShouldSolveExample()
        {
            // Arrange
            const int expectedScore = 15;

            // Act
            var score = await this.solver.SolveAsync(this.stream, CancellationToken.None).ConfigureAwait(false);

            // Assert
            score.Should().Be(expectedScore);
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "A Y",
                "B X",
                "C Z",
            };

            foreach (var line in lines)
            {
                await using (var writer = new StreamWriter(this.stream, Encoding.UTF8, leaveOpen: true))
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