using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day9
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayNine(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayNineRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();

            return services;
        }
    }
}