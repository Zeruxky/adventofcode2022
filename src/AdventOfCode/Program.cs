// See https://aka.ms/new-console-template for more information

using AdventOfCode;
using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using AdventOfCode.Day5;
using AdventOfCode.Day6;
using AdventOfCode.Day7;
using AdventOfCode.Day8;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var services = new ServiceCollection();
services.AddDayOne();
services.AddDayTwo();
services.AddDayThree();
services.AddDayFour();
services.AddDayFive();
services.AddDaySix();
services.AddDaySeven();
services.AddDayEight();
services.AddConsole();
services.AddInputDownloader();

await using (var serviceProvider = services.BuildServiceProvider())
{
    while (true)
    {
        var runners = serviceProvider.GetRequiredService<IEnumerable<IDayRunner>>();
        var days = Day.List.OrderBy(d => d.Value);
        var day = AnsiConsole.Prompt(
            new SelectionPrompt<Day>()
                .Title("Which day of the AdventOfCode 2022 you want to solve?")
                .MoreChoicesText("[grey](Move up and down to reveal more days)[/]")
                .UseConverter(d => $"Day {d.Name}")
                .AddChoices(days));
    
        var runner = runners.First(r => r.Day == day);
        await runner.RunAsync(CancellationToken.None).ConfigureAwait(false);

        if (!AnsiConsole.Confirm("Do you want to solve another puzzle from AdventOfCode 2022?"))
        {
            break;
        }
        
        AnsiConsole.Clear();
    }
}