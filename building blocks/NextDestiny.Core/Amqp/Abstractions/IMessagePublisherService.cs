namespace NextDestiny.Core.Amqp.Abstractions
{
    public interface IMessagePublisherService
    {
        Task Publish<T>(T message, string exchangeName, string exchangeType, string routeKey) where T : class;
    }
}
