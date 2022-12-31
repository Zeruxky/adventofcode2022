using Spectre.Console;

namespace AdventOfCode.Day7
{
    public class DaySevenRunner : DayRunnerBase<int, int>
    {
        public DaySevenRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Seven, solvers, console, downloader)
        {
        }
    }
}