namespace ProjectTemplate.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId, IReadOnlyCollection<CreateOrderItemCommand> Items);

public sealed record CreateOrderItemCommand(Guid ProductId, int Quantity, decimal UnitPrice);

public sealed record CreateOrderResult(Guid OrderId, Guid CustomerId, string Status, DateTime CreatedAtUtc);
