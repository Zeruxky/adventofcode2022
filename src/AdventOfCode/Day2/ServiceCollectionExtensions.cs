using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day2
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayTwo(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayTwoRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();
            services.AddSingleton<ISolver, PartTwoSolver>();
            
            return services;
        }
    }
}