using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.Database.MongoDb;
using Order.Application.Interfaces;
using Order.Application.Services;
using Order.Domain.Repositories;
using Order.Infrastructure.Persistence;
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

            var apiCatalogUri = configuration["ApiCatalogUri"]!;

            services
                .AddRefitClient<ICatalogApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiCatalogUri));

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
