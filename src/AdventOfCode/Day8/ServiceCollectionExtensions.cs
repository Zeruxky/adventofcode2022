using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day8
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayEight(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayEightRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();

            return services;
        }
    }
}