using Spectre.Console;

namespace AdventOfCode.Day8
{
    public class DayEightRunner : DayRunnerBase<int, int>
    {
        public DayEightRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Eight, solvers, console, downloader)
        {
        }
    }
}