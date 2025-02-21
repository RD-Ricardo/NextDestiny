namespace NextDestiny.Core.Amqp.Abstractions
{
    public interface IBusService
    {
        Task Publish<T>(T message) where T : class;
    }
}
