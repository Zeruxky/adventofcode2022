using AdventOfCode.Day3.PartOne;
using AdventOfCode.Day3.PartTwo;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day3
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayThree(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayThreeRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();

            return services;
        }
    }
}