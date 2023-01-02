using System.Threading.Channels;

namespace AdventOfCode.Day10
{
    public record CPU
    {
        private readonly ChannelWriter<int> channelWriter;
        private readonly Dictionary<char, Register> registers;
        private readonly List<int> cyclesOfInterest;
        private readonly List<SignalStrengthMeasurement> measurements;

        public CPU(IEnumerable<Register> registers, ChannelWriter<int> channelWriter)
        {
            this.channelWriter = channelWriter;
            this.registers = registers.ToDictionary(r => r.Id, r => r);
            this.measurements = new List<SignalStrengthMeasurement>();
            this.cyclesOfInterest = new List<int>();
            for (var i = 20; i <= 220; i += 40)
            {
                this.cyclesOfInterest.Add(i);
            }
        }

        public int Cycles { get; private set; }

        public IReadOnlyCollection<SignalStrengthMeasurement> Measurements => measurements;

        public void Stop()
        {
            this.channelWriter.Complete();
        }

        public async Task ExecuteAsync(Instruction instruction, CancellationToken ct)
        {
            if (instruction.Type == InstructionType.NoOp)
            {
                await this.IncreaseCycleAsync(ct).ConfigureAwait(false);
            }

            if (instruction.Type == InstructionType.AddX)
            {
                await this.IncreaseCycleAsync(ct).ConfigureAwait(false);
                await this.IncreaseCycleAsync(ct).ConfigureAwait(false);
                var register = this.registers['X'];
                register.Add(instruction.Value);
            }
        }

        private async Task IncreaseCycleAsync(CancellationToken ct)
        {
            this.Cycles++;
            await this.channelWriter.WriteAsync(this.registers['X'].Value, ct).ConfigureAwait(false);
            if (this.cyclesOfInterest.Contains(this.Cycles))
            {
                var addXRegister = this.registers['X'];
                this.TakeMeasurement(addXRegister);
            }
        }

        private void TakeMeasurement(Register register)
        {
            var measurement = new SignalStrengthMeasurement()
            {
                Cycle = this.Cycles,
                Value = register.Value * this.Cycles,
            };
            
            this.measurements.Add(measurement);
        }
    }
}