namespace AdventOfCode.Day10
{
    public record Instruction
    {
        public InstructionType Type { get; init; }
        
        public int Value { get; init; }
        
        public static bool TryParse(string value, out Instruction? instruction)
        {
            var separator = value.IndexOf(' ');
            var instructionName = separator == -1
                ? value
                : value[..separator];
            
            if (instructionName == "noop")
            {
                instruction = new Instruction()
                {
                    Type = InstructionType.NoOp,
                };
                return true;
            }

            if (instructionName == "addx")
            {
                var instructionValueSpan = value.AsSpan(separator + 1);
                if (int.TryParse(instructionValueSpan, out var instructionValue))
                {
                    instruction = new Instruction()
                    {
                        Type = InstructionType.AddX,
                        Value = instructionValue!,
                    };
                    return true;
                }
            }

            instruction = default;
            return false;
        }
    }
}