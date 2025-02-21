using System.Windows.Input;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Amqp.Abstractions;

namespace NextDestiny.Core.Amqp
{
    public static class AmqpConfiguration
    {
        public static IServiceCollection AddAmqpServices(this IServiceCollection services, IConfiguration configuration, params Type[] consumerTypes)
        {
            var amqpSettings = configuration.GetSection("AmqpSettings").Get<AmqpSettings>()!;

            GlobalTopology.Send.TryAddConvention(new RoutingKeySendTopologyConvention());

            services.AddMassTransit(x =>
            {
                foreach (var consumerType in consumerTypes)
                {
                    x.AddConsumer(consumerType);
                }

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(amqpSettings.Host, h =>
                    {
                        h.Username(amqpSettings.UserName);
                        h.Password(amqpSettings.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IBusService, BusService>();
            
            return services;
        }


    }
}
