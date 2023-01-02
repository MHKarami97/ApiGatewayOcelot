using ApiGateway.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.ConfigureOcelot(builder);

var host = builder.Host;
host.ConfigureMetric();

var services = builder.Services;
services.ConfigureIdentityServer(builder.Configuration);
services.AddOcelot(configuration);

var app = builder.Build();
await app.UseOcelot();
await app.RunAsync();