namespace AdventOfCode.Day2
{
    public record Strategy
    {
        public Strategy(Pick opponent, Pick self)
        {
            Opponent = opponent;
            Self = self;
        }

        public Pick Opponent { get; }
        
        public Pick Self { get; }
        
        public static bool TryParse(string line, out Strategy strategy)
        {
            if (!Pick.TryParse(line[0], out var opponent))
            {
                strategy = default;
                return false;
            }
            
            var index = line.AsSpan().LastIndexOf(' ');
            if (!Pick.TryParse(line[index + 1], out var self))
            {
                strategy = default;
                return false;
            }
            
            strategy = new Strategy(opponent!, self!);
            return true;
        }
    }
}