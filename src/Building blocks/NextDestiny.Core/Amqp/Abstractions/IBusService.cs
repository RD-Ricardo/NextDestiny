namespace NextDestiny.Core.Amqp.Abstractions
{
    public interface IBusService
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}
