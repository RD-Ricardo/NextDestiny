using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NextDestiny.Core.WebApi.Extensions
{
    public static class ObservabilityExtensions
    {
        public static WebApplicationBuilder AddObservability(this WebApplicationBuilder builder, string applicationName)
        {
            builder.AddSerilog(builder.Configuration, applicationName);

            builder.Services.AddElasticApmForAspNetCore(
                new HttpDiagnosticsSubscriber(),
                new MongoDbDiagnosticsSubscriber());

            return builder;
        }
    }
}
