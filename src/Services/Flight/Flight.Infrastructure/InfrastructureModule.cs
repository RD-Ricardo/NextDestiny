using Flight.Application.Services;
using Flight.Domain.Repositories;
using Flight.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.Database.MongoDb;

namespace Flight.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDb(configuration);
            
            services.AddScoped<IFlightBookingRepository, FlightBookingRepository>();
            
            services.AddScoped<IFlightService, FlightService>();

            return services;
        }
    }
}
