using System.Text;
using Microsoft.Extensions.Primitives;
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
                var stringBuilder = new StringBuilder();
                foreach (var line in lines)
                {
                    var formattedCharacters = line.Select(c =>
                    {
                        var content = c == '#'
                            ? $"[lime]{c}[/]"
                            : c.ToString();

                        return content;
                    });

                    stringBuilder.AppendJoin(string.Empty, formattedCharacters);
                    stringBuilder.AppendLine();
                }
                this.console.WriteLine($"Result of Day {solver.Day} Part {solver.Part}:");
                this.console.Markup(stringBuilder.ToString());
            }
        }
    }
}