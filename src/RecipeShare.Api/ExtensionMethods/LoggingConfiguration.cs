using Serilog;

namespace RecipeShare.ExtensionMethods;

public static class LoggingConfiguration
{
    public static IHostBuilder AddSerilogConfiguration(this IHostBuilder host)
    {
        
        host.UseSerilog((context, configuration) => configuration
            .WriteTo.Console()
            .MinimumLevel.Information());
            
        return host;
    }
}