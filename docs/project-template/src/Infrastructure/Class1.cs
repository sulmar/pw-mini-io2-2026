namespace ProjectTemplate.Infrastructure.Orders;

using System.Collections.Concurrent;
using ProjectTemplate.Application.Orders;
using ProjectTemplate.Domain.Orders;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
