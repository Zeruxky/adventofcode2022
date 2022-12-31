using Spectre.Console;

namespace AdventOfCode.Day1
{
    public class DayOneRunner : DayRunnerBase<int, int>
    {
        public DayOneRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader inputDownloader)
            : base(Day.One, solvers, console, inputDownloader)
        {
        }
    }
}