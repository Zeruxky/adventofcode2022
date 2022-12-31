namespace AdventOfCode
{
    public record AdventOfCodeOptions
    {
        public const string Key = "AdventOfCode";

        public Uri BaseAddress { get; init; } = new Uri("https://adventofcode.com/2022/");

        public string SessionToken { get; init; } = string.Empty;
    }
}