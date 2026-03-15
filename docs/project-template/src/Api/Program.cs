using ProjectTemplate.Api.Contracts;
using ProjectTemplate.Application.Orders;
using ProjectTemplate.Application.Orders.CreateOrder;
using ProjectTemplate.Infrastructure.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddScoped<CreateOrderHandler>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    message = "Order management service is running.",
    implementedUseCase = "UC01 Create Order"
}));

app.MapPost("/orders", async (CreateOrderRequest request, CreateOrderHandler handler, CancellationToken cancellationToken) =>
{
    try
    {
        var command = new CreateOrderCommand(
            request.CustomerId,
            request.Items
                .Select(item => new CreateOrderItemCommand(item.ProductId, item.Quantity, item.UnitPrice))
                .ToArray());

        var result = await handler.HandleAsync(command, cancellationToken);

        return Results.Created($"/orders/{result.OrderId}", result);
    }
    catch (ArgumentException exception)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["request"] = new[] { exception.Message }
        });
    }
});

app.Run();

public partial class Program;
