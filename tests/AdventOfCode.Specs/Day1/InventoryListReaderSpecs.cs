using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day1;
using FluentAssertions;
using Xunit;

namespace AdventOfCode.Specs.Day1
{
    [Trait("Category", "UnitTests")]
    public class InventoryListReaderSpecs : IAsyncLifetime
    {
        private readonly Stream stream;
        private readonly InventoryListReader reader;

        public InventoryListReaderSpecs()
        {
            this.stream = new MemoryStream();
            this.reader = new InventoryListReader(this.stream, true);
        }

        public static IEnumerable<object[]> Data => new[]
        {
            new[] { string.Empty },
            new[] { " " },
            new[] { Environment.NewLine},
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void ShouldDetectSeparatorLine(string input)
        { 
            // Act
            var isSeparatorLine = InventoryListReader.IsSeparatorLine(input);

            // Assert
            isSeparatorLine.Should().BeTrue();
        }

        public async Task InitializeAsync()
        {
            var lines = new[]
            {
                "1000",
                "2000",
                "3000",
                string.Empty,
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

        public Task DisposeAsync()
        {
            this.reader.Dispose();
            return Task.CompletedTask;
        }
    }
}