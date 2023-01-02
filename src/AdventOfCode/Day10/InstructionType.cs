using Ardalis.SmartEnum;

namespace AdventOfCode.Day10
{
    public class InstructionType : SmartEnum<InstructionType>
    {
        public static readonly InstructionType NoOp = new InstructionType(nameof(NoOp), 0);
        public static readonly InstructionType AddX = new InstructionType(nameof(AddX), 1);
        
        private InstructionType(string name, int value)
            : base(name, value)
        {
        }
    }
}