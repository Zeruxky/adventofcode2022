using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Day10
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDayTen(this IServiceCollection services)
        {
            services.AddSingleton<IDayRunner, DayTenRunner>();
            services.AddSingleton<ISolver, PartOneSolver>();

            return services;
        }
    }
}