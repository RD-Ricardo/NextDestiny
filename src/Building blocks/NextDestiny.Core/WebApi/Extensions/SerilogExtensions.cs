using Elastic.Apm.SerilogEnricher;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;

namespace NextDestiny.Core.WebApi.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string applicationName)
        {
            Configuration(configuration, applicationName);
            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Log.Logger, true);

            return builder;
        }

        public static HostApplicationBuilder AddSerilog(this HostApplicationBuilder builder, IConfiguration configuration, string applicationName)
        {
            Configuration(configuration, applicationName);
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger, true);

            return builder;
        }

        private static void Configuration(IConfiguration configuration, string applicationName)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(configuration)
               .Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}")
               .Enrich.FromLogContext()
               .Enrich.WithMachineName()
               .Enrich.WithEnvironmentUserName()
               .Enrich.WithElasticApmCorrelationInfo()
               .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
               .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("specific error"))
               .WriteTo.Async(writeTo => writeTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearchSettings:Uri"]))
               {
                   TypeName = null,
                   AutoRegisterTemplate = true,
                   IndexFormat = "indexlogs",
                   BatchAction = ElasticOpType.Create,
                   ModifyConnectionSettings = x => x.BasicAuthentication(configuration["ElasticSearchSettings:Username"], configuration["ElasticSearchSettings:Password"])
               }))
               .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
               .WriteTo.Debug()
               .CreateLogger();
        }
    }
}
