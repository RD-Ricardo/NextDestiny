using Catalog.Application.Services;
using Catalog.Application.Services.Interfaces;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Database.MongoDb;

namespace Catalog.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfraCatalog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Application.Mappers.ProductProfile));
         
            services.AddMongoDb(configuration);
            
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
