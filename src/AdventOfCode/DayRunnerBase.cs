using Spectre.Console;

namespace AdventOfCode
{
    public abstract class DayRunnerBase<T1, T2> : IDayRunner
    {
        private readonly string path;
        private readonly string directory;
        private readonly ISolver[] solvers;
        private readonly IInputDownloader downloader;
        protected readonly IAnsiConsole console;

        protected DayRunnerBase(Day day, IEnumerable<ISolver> solvers, IAnsiConsole console, IInputDownloader downloader)
        {
            this.Day = day;
            this.solvers = solvers.Where(s => s.Day == this.Day).ToArray();
            this.directory = "puzzle_input";
            this.path = Path.Combine(this.directory, $"input_{day}.txt");
            this.console = console;
            this.downloader = downloader;
        }
        
        public Day Day { get; }
        
        public async Task RunAsync(CancellationToken ct)
        {
            await this.InitializeAsync(ct).ConfigureAwait(false);
            var part = this.GetPart();
            if (part == Part.One || part == Part.All)
            {
                var solver = this.solvers.OfType<ISolver<T1>>().First(s => s.Part == Part.One);
                await this.SolvePartOneAsync(solver, ct).ConfigureAwait(false);
            }

            if (part == Part.Two || part == Part.All)
            {
                var solver = this.solvers.OfType<ISolver<T2>>().First(s => s.Part == Part.Two);
                await this.SolvePartTwoAsync(solver, ct).ConfigureAwait(false);
            }
        }

        protected virtual async Task SolvePartOneAsync(ISolver<T1> solver, CancellationToken ct)
        {
            await using (var file = this.GetInputFile())
            {
                var result = await solver.SolveAsync(file, ct).ConfigureAwait(false);
                this.console.WriteLine($"Result of Day {solver.Day} Part {solver.Part}: {result}");
            }
        }
        
        protected virtual async Task SolvePartTwoAsync(ISolver<T2> solver, CancellationToken ct)
        {
            await using (var file = this.GetInputFile())
            {
                var result = await solver.SolveAsync(file, ct).ConfigureAwait(false);
                this.console.WriteLine($"Result of Day {solver.Day} Part {solver.Part}: {result}");
            }
        }

        protected Stream GetInputFile()
        {
            return new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.None);
        }
        
        private Part GetPart()
        {
            var part = this.console.Prompt(
                new SelectionPrompt<Part>()
                    .Title($"Which part of the day {this.Day} you want to solve?")
                    .MoreChoicesText("[grey](Move up and down to reveal more parts)[/]")
                    .UseConverter(p => p == Part.All ? "All parts" : $"Part {p.Name}")
                    .AddChoices(Part.List));
            
            return part;
        }

        private async Task InitializeAsync(CancellationToken ct)
        {
            if (!Directory.Exists(this.directory))
            {
                Directory.CreateDirectory(this.directory);
            }
            
            if (File.Exists(this.path))
            {
                return;
            }

            await using (var input = await this.downloader.GetAsStreamAsync(this.Day, ct).ConfigureAwait(false))
            await using (var file = new FileStream(this.path, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                await input.CopyToAsync(file, ct).ConfigureAwait(false);
            }
        }
    }
}