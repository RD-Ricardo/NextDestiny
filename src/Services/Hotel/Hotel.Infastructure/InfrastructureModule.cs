using Hotel.Application.Services;
using Hotel.Domain.Repositoties;
using Hotel.Infastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Database.MongoDb;

namespace Hotel.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDb(configuration);
            
            services.AddScoped<IBookingRepository,BookingRepository>();
            
            services.AddScoped<IBookingService, BookingService>();

            return services;
        }
    }
}
