using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day5;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day5
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
            const string expectedMessage = "CMZ";

            // Act
            var message = await this.solver.SolveAsync(this.stream, CancellationToken.None).ConfigureAwait(false);

            // Assert
            message.Should().Be(expectedMessage);
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "    [D]    ",
                "[N] [C]    ",
                "[Z] [M] [P]",
                " 1   2   3 ",
                Environment.NewLine,
                "move 1 from 2 to 1",
                "move 3 from 1 to 3",
                "move 2 from 2 to 1",
                "move 1 from 1 to 2",
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