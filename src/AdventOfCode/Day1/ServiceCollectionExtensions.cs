using AdventOfCode.Day1.PartOne;
using AdventOfCode.Day1.PartTwo;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day1
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayOne(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayOneRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();
            return services;
        }
    }
}