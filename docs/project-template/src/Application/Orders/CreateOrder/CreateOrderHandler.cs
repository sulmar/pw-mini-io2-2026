namespace ProjectTemplate.Application.Orders.CreateOrder;

using ProjectTemplate.Domain.Orders;

public sealed class CreateOrderHandler(IOrderRepository orderRepository)
{
    public async Task<CreateOrderResult> HandleAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = Order.Create(
            command.CustomerId,
            command.Items.Select(item => new OrderItem(item.ProductId, item.Quantity, item.UnitPrice)),
            DateTime.UtcNow);

        await orderRepository.AddAsync(order, cancellationToken);

        return new CreateOrderResult(order.Id, order.CustomerId, order.Status.ToString(), order.CreatedAtUtc);
    }
}
