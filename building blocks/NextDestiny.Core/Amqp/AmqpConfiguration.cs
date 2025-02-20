using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Amqp.Abstractions;

namespace NextDestiny.Core.Amqp
{
    public static class AmqpConfiguration
    {
        public static IServiceCollection AddAmqpServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessagePublisherService, MessagePublisherService>();

            return services;
        }
    }
}
