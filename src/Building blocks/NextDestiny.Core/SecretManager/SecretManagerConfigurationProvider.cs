using Amazon;
using System.Text.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace NextDestiny.Core.SecretManager
{
    public class SecretManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly string _secretName;

        private readonly string _region = "us-east-1";  
        public SecretManagerConfigurationProvider(string secretName)
        {
            _secretName = secretName;
        }

        public override void Load()
        {
            GetSecretValueRequest request = new GetSecretValueRequest()
            {
                SecretId = _secretName,
                VersionStage = "AWSCURRENT"
            };

            GetSecretValueResponse response;

            using var client = new AmazonSecretsManagerClient(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"), Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"), RegionEndpoint.GetBySystemName(_region));

            try
            {
                response = client.GetSecretValueAsync(request).Result;
                Data = JsonSerializer.Deserialize<Dictionary<string, string>>(DecodeString(response))!;
            }
            catch (AmazonSecretsManagerException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public static string DecodeString(GetSecretValueResponse response)
        {
            if (response.SecretString is not null)
            {
                var secret = response.SecretString;
                return secret;
            }
            else if (response.SecretBinary is not null)
            {
                var memoryStream = response.SecretBinary;
                StreamReader reader = new StreamReader(memoryStream);
                string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                return decodedBinarySecret;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
