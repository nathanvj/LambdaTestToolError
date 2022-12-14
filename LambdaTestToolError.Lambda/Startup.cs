using LambdaTestToolError.API.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LambdaTestToolError.Lambda;

public class Startup
{
    public static IServiceProvider Services { get; private set; }

    public static void ConfigureServices()
    {
        ServiceCollection services = new();
        IConfiguration configuration = GetConfiguration();

        services.Configure<CustomOptions>(configuration.GetSection(nameof(CustomOptions)));

        Services = services.BuildServiceProvider();
    }

    private static IConfiguration GetConfiguration()
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{environment}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();
    }
}
