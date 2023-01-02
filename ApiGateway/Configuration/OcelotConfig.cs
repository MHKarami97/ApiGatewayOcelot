using Ocelot.DependencyInjection;

namespace ApiGateway.Configuration;

public static class OcelotConfig
{
    public static void ConfigureOcelot(this ConfigurationManager configuration, WebApplicationBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentException("not valid builder", nameof(builder));
        }

        _ = configuration.AddOcelot($"Routes/{builder.Environment.EnvironmentName}", builder.Environment);

        _ = builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
    }
}