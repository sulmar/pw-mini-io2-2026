using ProjectTemplate.Domain.Orders;

namespace ProjectTemplate.UnitTests;

public class OrderTests
{
    [Fact]
    public void Create_ShouldInitializeOrderWithCreatedStatus()
    {
        var customerId = Guid.NewGuid();
        var createdAtUtc = new DateTime(2026, 3, 15, 10, 30, 0, DateTimeKind.Utc);
        var items = new[]
        {
            new OrderItem(Guid.NewGuid(), 2, 19.99m)
        };

        var order = Order.Create(customerId, items, createdAtUtc);

        Assert.Equal(customerId, order.CustomerId);
        Assert.Equal(OrderStatus.Created, order.Status);
        Assert.Equal(createdAtUtc, order.CreatedAtUtc);
        Assert.Single(order.Items);
    }

    [Fact]
    public void Create_ShouldRejectEmptyOrder()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            Order.Create(Guid.NewGuid(), Array.Empty<OrderItem>(), DateTime.UtcNow));

        Assert.Equal("items", exception.ParamName);
    }
}
