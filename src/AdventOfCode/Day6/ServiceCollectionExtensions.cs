using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day6
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDaySix(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DaySixRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();

            return services;
        }
    }
}