using Spectre.Console;

namespace AdventOfCode.Day5
{
    public class DayFiveRunner : DayRunnerBase<string, string>
    {
        public DayFiveRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
            : base(Day.Five, solvers, console, downloader)
        {
        }
    }
}