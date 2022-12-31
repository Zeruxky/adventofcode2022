using Spectre.Console;

namespace AdventOfCode.Day2
{
    public class DayTwoRunner : DayRunnerBase<int, int>
    {
        public DayTwoRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Two, solvers, console, downloader)
        {
        }
    }
}