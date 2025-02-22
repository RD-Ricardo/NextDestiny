namespace Order.Application.Dtos
{
    public record OrderCreateDto(CustomerCreateDto Customer, List<OrderItemCreateDto> Items);
    public record OrderItemCreateDto(Guid ProductId, int Quantity);
}
