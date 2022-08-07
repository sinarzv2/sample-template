using System;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace SampleTemplate.Extentions
{
    public static class ConfigureHostBuilderExtentions
    {
        public static void UseCustomSerilog(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
