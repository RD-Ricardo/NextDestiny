using Microsoft.Extensions.Configuration;

namespace NextDestiny.Core.SecretManager
{
    public class SecretManagerSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
           return new SecretManagerConfigurationProvider("next-destiny-sc");
        }
    }
}
