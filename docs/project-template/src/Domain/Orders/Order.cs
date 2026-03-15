namespace ProjectTemplate.Domain.Orders;

public sealed class Order
{
    private readonly List<OrderItem> _items;

    private Order(Guid id, Guid customerId, DateTime createdAtUtc, List<OrderItem> items)
    {
        Id = id;
        CustomerId = customerId;
        CreatedAtUtc = createdAtUtc;
        _items = items;
        Status = OrderStatus.Created;
    }

    public Guid Id { get; }

    public Guid CustomerId { get; }

    public DateTime CreatedAtUtc { get; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    public static Order Create(Guid customerId, IEnumerable<OrderItem> items, DateTime createdAtUtc)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentException("CustomerId must be provided.", nameof(customerId));
        }

        var itemList = items.ToList();

        if (itemList.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one item.", nameof(items));
        }

        return new Order(Guid.NewGuid(), customerId, createdAtUtc, itemList);
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Shipped)
        {
            throw new InvalidOperationException("Shipped orders cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }
}
