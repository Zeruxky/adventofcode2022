using AdventOfCode.Day4.PartOne;
using AdventOfCode.Day4.PartTwo;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day4
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayFour(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayFourRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();
            
            return services;
        }
    }
}