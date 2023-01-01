using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AdventOfCode
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddConsole(this IServiceCollection services)
        {
            services.AddSingleton(AnsiConsole.Console);
            return services;
        }
        
        internal static IServiceCollection AddInputDownloader(this IServiceCollection services)
        {
            var settings = GetSettings();
            if (string.IsNullOrEmpty(settings.SessionToken))
            {
                throw new InvalidOperationException("A session token is required to download puzzle input.");
            }
            
            services.AddHttpClient<IInputDownloader, InputDownloader>(client =>
            {
                client.BaseAddress = settings.BaseAddress;
                client.DefaultRequestHeaders.Add("Cookie", settings.SessionToken);
            });

            return services;
        }

        private static AdventOfCodeSettings GetSettings()
        {
            using (var file = File.OpenRead("adventofcode_settings.yml"))
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                var yml = reader.ReadToEnd();
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                var settings = deserializer.Deserialize<AdventOfCodeSettings>(yml);
                return settings;
            }
        }
    }
}