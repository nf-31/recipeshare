using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace RecipeShare.Factories;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
 
        var host = builder.Build();
        host.Start();
        return host;
    }
}