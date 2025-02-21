using Microsoft.Extensions.Configuration;

namespace NextDestiny.Core.SecretManager
{
    public class SecretManagerSource : IConfigurationSource
    {
        public string _secretName { get; set; }
        public SecretManagerSource(string secretName)
        {
            _secretName = secretName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
           return new SecretManagerConfigurationProvider(_secretName);
        }
    }
}
