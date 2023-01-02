namespace AdventOfCode.Day10
{
    public record Cpu
    {
        private readonly Dictionary<char, Register> registers;
        private readonly List<int> cyclesOfInterest;
        private readonly List<SignalStrengthMeasurement> measurements;

        public Cpu(IEnumerable<Register> registers)
        {
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