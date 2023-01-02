using System.Threading.Channels;

namespace AdventOfCode.Day10
{
    public class PartOneSolver : ISolver<int>
    {
        public Day Day => Day.Ten;
        public Part Part => Part.One;
        
        public async Task<int> SolveAsync(Stream stream, CancellationToken ct)
        {
            if (stream.Length == 0)
            {
                return 0;
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read from write-only stream.", nameof(stream));
            }

            using (var reader = new InstructionReader(stream, true))
            {
                var registers = new[]
                {
                    new Register()
                    {
                        Id = 'X',
                    },
                };
            
                var cpu = new CPU(registers, Channel.CreateUnbounded<int>());
                var instructions = reader.ReadAllAsync(ct);
                await foreach (var instruction in instructions.WithCancellation(ct).ConfigureAwait(false))
                {
                    await cpu.ExecuteAsync(instruction, ct).ConfigureAwait(false);
                }

                var totalSignalStrength = cpu.Measurements.Sum(m => m.Value);
                return totalSignalStrength;
            }
        }
    }
}