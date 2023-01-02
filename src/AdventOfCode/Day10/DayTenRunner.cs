using Spectre.Console;

namespace AdventOfCode.Day10
{
    public class DayTenRunner : DayRunnerBase<int, IEnumerable<string>>
    {
        public DayTenRunner(IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader) 
            : base(Day.Ten, solvers, console, downloader)
        {
        }

        protected override async Task SolvePartTwoAsync(ISolver<IEnumerable<string>> solver, CancellationToken ct)
        {
            await using (var file = this.GetInputFile())
            {
                var lines = await solver.SolveAsync(file, ct).ConfigureAwait(false);
                this.console.WriteLine($"Result of Day {solver.Day} Part {solver.Part}:");
                foreach (var line in lines)
                {
                    this.console.WriteLine(line);
                }
            }
        }
    }
}