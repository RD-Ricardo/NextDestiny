using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.Database.MongoDb;
using Order.Application.Interfaces;
using Order.Application.Services;
using Refit;

namespace Order.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfraOrder(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(typeof(Application.Mappers.ProductProfile));
         
            services.AddMongoDb(configuration);
            
            services.AddAmqpServices(configuration);

            services
                .AddRefitClient<ICatalogApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiCatalogUri"]!));

            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
