using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day6;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day6
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
            const int expectedPosition = 7;

            // Act
            var position = await this.solver.SolveAsync(this.stream, CancellationToken.None).ConfigureAwait(false);

            // Assert
            position.Should().Be(expectedPosition);
        }

        public async Task InitializeAsync()
        {
            await using (var writer = new StreamWriter(this.stream, Encoding.UTF8, leaveOpen: true))
            {
                const string line = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
                await writer.WriteLineAsync(line.AsMemory(), CancellationToken.None).ConfigureAwait(false);
            }

            this.stream.Seek(0, SeekOrigin.Begin);
        }

        public async Task DisposeAsync()
        {
            await this.stream.DisposeAsync().ConfigureAwait(false);
        }
    }
}