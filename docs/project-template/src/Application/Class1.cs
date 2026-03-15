namespace ProjectTemplate.Application.Orders;

using ProjectTemplate.Domain.Orders;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
}
