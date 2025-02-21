using Microsoft.Extensions.Configuration;

namespace NextDestiny.Core.SecretManager
{
    public static class SecretManagerConfiguration
    {
        public static void AddSecretManager(this IConfigurationBuilder configurationBuilder, string secretName)
        {
            configurationBuilder.Add(new SecretManagerSource(secretName));
        }
    }
}
