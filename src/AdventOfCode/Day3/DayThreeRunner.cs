using Spectre.Console;

namespace AdventOfCode.Day3
{
    public class DayThreeRunner : DayRunnerBase<int, int>
    {
        public DayThreeRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader) 
            : base(Day.Three, solvers, console, downloader)
        {
        }
    }
}