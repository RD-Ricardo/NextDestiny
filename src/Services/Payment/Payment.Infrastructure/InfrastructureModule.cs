using Microsoft.Extensions.DependencyInjection;

namespace Payment.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            return services;
        }
    }
}
