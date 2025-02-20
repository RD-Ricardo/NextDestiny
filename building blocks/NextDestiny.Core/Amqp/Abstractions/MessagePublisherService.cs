using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace NextDestiny.Core.Amqp.Abstractions
{
    public class MessagePublisherService : IMessagePublisherService
    {
        private readonly IConnectionFactory _connectionFactory;

        public MessagePublisherService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Publish<T>(T message, string exchangeName, string exchangeType, string routeKey) where T : class
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            using var channel = await connection.CreateChannelAsync();
            
            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: exchangeType);

            var messageBody = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(messageBody);

            await channel.BasicPublishAsync(exchangeName, routeKey, body, default);
        }
    }
}
