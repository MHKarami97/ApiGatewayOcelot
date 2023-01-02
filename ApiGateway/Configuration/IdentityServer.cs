using ApiGateway.Authentication;

namespace ApiGateway.Configuration;

public static class IdentityServer
{
    public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        const string authenticationProviderKey = "IdentityServer";

        _ = services.AddAuthentication()
            .AddScheme<SystemKeyAuthenticationOptions, SystemKeyAuthenticationHandler>(authenticationProviderKey,
            op => configuration.Bind(authenticationProviderKey, op));
    }
}