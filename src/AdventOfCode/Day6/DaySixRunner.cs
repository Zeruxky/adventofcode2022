using Spectre.Console;

namespace AdventOfCode.Day6
{
    public class DaySixRunner : DayRunnerBase<int, int>
    {
        public DaySixRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Six, solvers, console, downloader)
        {
        }
    }
}