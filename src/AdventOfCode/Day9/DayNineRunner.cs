using Spectre.Console;

namespace AdventOfCode.Day9
{
    public class DayNineRunner : DayRunnerBase<int, int>
    {
        public DayNineRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Nine, solvers, console, downloader)
        {
        }
    }
}