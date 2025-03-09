namespace Payment.Application.Interfaces
{
    public interface IPaymentService
    {
        Task ProcessAsync(Guid orderId);
    }
}
