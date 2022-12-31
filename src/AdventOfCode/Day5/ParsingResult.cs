namespace AdventOfCode.Day5
{
    public class ParsingResult
    {
        public int Position { get; init; }
        
        public char Value { get; init; }

        public static IEnumerable<ParsingResult> Parse(string line)
        {
            var position = 0;
            var charactersRead = 0;
            foreach (var character in line)
            {
                charactersRead++;
                if (charactersRead == 4)
                {
                    position++;
                    charactersRead = 0;
                }
                
                if (!char.IsLetter(character))
                {
                    continue;
                }

                var result = new ParsingResult()
                {
                    Position = position,
                    Value = character,
                };

                yield return result;
            }
        }
    }
}