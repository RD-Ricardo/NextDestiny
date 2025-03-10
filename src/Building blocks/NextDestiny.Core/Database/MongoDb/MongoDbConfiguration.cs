using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Elastic.Apm.MongoDb;
using MongoDB.Driver.Core.Events;

namespace NextDestiny.Core.Database.MongoDb
{
    public static class MongoDbConfiguration
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(options => configuration.GetSection("MongoDbSettings").Bind(options));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

                var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));

                clientSettings.ClusterConfigurator = builder => builder.Subscribe(new MongoDbEventSubscriber());
              
                return new MongoClient(clientSettings);
            });


            services.AddScoped(delegate (IServiceProvider provider)
            {
                var client = provider.GetRequiredService<IMongoClient>();
                var settings = provider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return client.GetDatabase(settings.DatabaseName);
            });

            return services;
        }
    }
}
