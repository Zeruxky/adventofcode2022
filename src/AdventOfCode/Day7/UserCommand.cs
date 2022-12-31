using System.Text.RegularExpressions;

namespace AdventOfCode.Day7
{
    public record UserCommand
    {
        private static readonly Regex Regex = new Regex(@"^\$\s(?<command>\w+?)(?:\s(?<parameters>.+?))?$");
        
        public UserCommandType Type { get; init; }
        
        public string Parameters { get; init; }

        public static bool TryParse(string value, out UserCommand? command)
        {
            var match = Regex.Match(value);
            if (!match.Success)
            {
                command = default;
                return false;
            }
            
            if (!UserCommandType.TryParse(match.Groups["command"].Value, out var commandType))
            {
                command = default;
                return false;
            }

            command = new UserCommand()
            {
                Type = commandType!,
                Parameters = match.Groups["parameters"].Value,
            };
            return true;
        }
    }
}