using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day7
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDaySeven(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DaySevenRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();

            return services;
        }
    }
}