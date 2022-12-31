using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day5
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayFive(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayFiveRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();

            return services;
        }
    }
}