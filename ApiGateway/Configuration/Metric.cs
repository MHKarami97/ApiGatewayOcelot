using App.Metrics;
using App.Metrics.AspNetCore;

namespace ApiGateway.Configuration;

public static class Metric
{
    public static void ConfigureMetric(this IHostBuilder hostBuilder)
    {
        var metrics = new MetricsBuilder()
            .OutputMetrics.AsPrometheusPlainText()
            .Build();

        _ = hostBuilder.ConfigureMetrics(metrics)
            .UseMetrics();
    }
}