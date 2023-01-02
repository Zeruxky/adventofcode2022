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
            
            this.cpu = new CPU(registers);
            this.crt = new CRT();
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
                var instructions = reader.ReadAllAsync(ct);
                await foreach (var instruction in instructions.WithCancellation(ct).ConfigureAwait(false))
                {
                    this.cpu.Execute(instruction);
                }

                var displayLines = crt.Draw(this.cpu.RegisterHistory);
                return displayLines;
            }
        }
    }
}