using System.Threading.Channels;

namespace AdventOfCode.Day10
{
    public record RegisterMeasurement
    {
        public int Cycle { get; init; }
        
        public int Value { get; init; }
    }

    public record CPU
    {
        private readonly List<RegisterMeasurement> registerMeasurements;
        private readonly Dictionary<char, Register> registers;
        private readonly List<int> cyclesOfInterest;
        private readonly List<SignalStrengthMeasurement> measurements;

        public CPU(IEnumerable<Register> registers)
        {
            this.registerMeasurements = new List<RegisterMeasurement>();
            this.registers = registers.ToDictionary(r => r.Id, r => r);
            this.measurements = new List<SignalStrengthMeasurement>();
            this.cyclesOfInterest = new List<int>();
            for (var i = 20; i <= 220; i += 40)
            {
                this.cyclesOfInterest.Add(i);
            }
        }

        public int Cycles { get; private set; }

        public IReadOnlyCollection<SignalStrengthMeasurement> Measurements => this.measurements;

        public IReadOnlyCollection<RegisterMeasurement> RegisterHistory => this.registerMeasurements;

        public void Execute(Instruction instruction)
        {
            if (instruction.Type == InstructionType.NoOp)
            {
                this.IncreaseCycle();
            }

            if (instruction.Type == InstructionType.AddX)
            {
                this.IncreaseCycle();
                this.IncreaseCycle();
                var register = this.registers['X'];
                register.Add(instruction.Value);
            }
        }

        private void IncreaseCycle()
        {
            this.Cycles++;
            var xRegister = this.registers['X'];
            this.TakeMeasurementOfRegister(xRegister);
            if (this.cyclesOfInterest.Contains(this.Cycles))
            {
                this.TakeMeasurementOfSignalStrength(xRegister);
            }
        }

        private void TakeMeasurementOfRegister(Register register)
        {
            var measurement = new RegisterMeasurement()
            {
                Cycle = this.Cycles,
                Value = register.Value,
            };
            
            this.registerMeasurements.Add(measurement);
        }

        private void TakeMeasurementOfSignalStrength(Register register)
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