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
                var result = await solver.SolveAsync(file, ct).ConfigureAwait(false);
                var displayLines = result.ToArray();
                var width = displayLines.Max(x => x.Length);
                var height = displayLines.Length;
                var canvas = new Canvas(width, height);
                var lineCount = 0;
                foreach (var displayLine in displayLines)
                {
                    for (var i = 0; i < canvas.Width; i++)
                    {
                        var character = displayLine[i];
                        if (character == '#')
                        {
                            canvas.SetPixel(i, lineCount, Color.Red);
                        }
                    }

                    lineCount++;
                }
                
                this.console.Write(canvas);
            }
        }
    }
}