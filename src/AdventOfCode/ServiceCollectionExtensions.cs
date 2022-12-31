using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace AdventOfCode
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsole(this IServiceCollection services)
        {
            services.AddSingleton(AnsiConsole.Console);
            return services;
        }
        
        public static IServiceCollection AddInputDownloader(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdventOfCodeOptions>(configuration.GetSection(AdventOfCodeOptions.Key));
            services.AddHttpClient<IInputDownloader, InputDownloader>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<AdventOfCodeOptions>>();
                client.BaseAddress = options.Value.BaseAddress;
                client.DefaultRequestHeaders.Add("Cookie", options.Value.SessionToken);
            });

            return services;
        }
    }
}