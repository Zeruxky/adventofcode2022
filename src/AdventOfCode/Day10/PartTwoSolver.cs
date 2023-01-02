using System.Text.Unicode;
using System.Threading.Channels;

namespace AdventOfCode.Day10
{
    public class PartTwoSolver : ISolver<IEnumerable<string>>
    {
        private readonly CPU cpu;
        private readonly CRT crt;

        public PartTwoSolver()
        {
            var registers = new[]
            {
                new Register()
                {
                    Id = 'X',
                },
            };

            var channel = Channel.CreateUnbounded<int>();
            this.cpu = new CPU(registers, channel.Writer);
            this.crt = new CRT(channel.Reader);
        }

        public Day Day => Day.Ten;

        public Part Part => Part.Two;

        public async Task<IEnumerable<string>> SolveAsync(Stream stream, CancellationToken ct)
        {
            if (stream.Length == 0)
            {
                return Enumerable.Empty<string>();
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read from write-only stream.", nameof(stream));
            }

            using (var reader = new InstructionReader(stream, true))
            {
                var crtTask = this.crt.RunAsync(ct);
                var instructions = reader.ReadAllAsync(ct);
                await foreach (var instruction in instructions.WithCancellation(ct).ConfigureAwait(false))
                {
                    await this.cpu.ExecuteAsync(instruction, ct).ConfigureAwait(false);
                }

                this.cpu.Stop();
                await crtTask.ConfigureAwait(false);
                var displayLines = await this.crt.PrintAsync(ct)
                    .ToArrayAsync(ct)
                    .ConfigureAwait(false);

                return displayLines;
            }
        }
    }
}