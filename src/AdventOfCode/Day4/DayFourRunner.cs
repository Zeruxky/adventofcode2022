using Spectre.Console;

namespace AdventOfCode.Day4
{
    public class DayFourRunner : DayRunnerBase<int, int>
    {
        public DayFourRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Four, solvers, console, downloader)
        {
        }
    }
}