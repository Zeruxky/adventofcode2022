using Ardalis.SmartEnum;

namespace AdventOfCode.Day7
{
    public class UserCommandType : SmartEnum<UserCommandType>
    {
        public static readonly UserCommandType ChangeDirectory = new UserCommandType(nameof(ChangeDirectory), 0, "cd");
        
        public static readonly UserCommandType ListItems = new UserCommandType(nameof(ListItems), 1, "ls");

        private UserCommandType(string name, int value, string shortcut)
            : base(name, value)
        {
            this.Shortcut = shortcut;
        }
        
        public string Shortcut { get; }

        public static bool TryParse(string value, out UserCommandType? commandType)
        {
            var userCommandType = UserCommandType.List.FirstOrDefault(u => u.Shortcut.Equals(value));
            if (userCommandType is null)
            {
                commandType = default;
                return false;
            }

            commandType = userCommandType;
            return true;
        }
    }
}