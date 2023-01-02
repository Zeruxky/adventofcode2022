using Spectre.Console;

namespace AdventOfCode.Day10
{
    public class DayTenRunner : DayRunnerBase<int, int>
    {
        public DayTenRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader) 
            : base(Day.Ten, solvers, console, downloader)
        {
        }
    }
}