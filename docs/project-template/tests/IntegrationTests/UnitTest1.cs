using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProjectTemplate.IntegrationTests;

public class CreateOrderEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CreateOrderEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostOrders_ShouldReturnCreated()
    {
        var request = new
        {
            customerId = Guid.NewGuid(),
            items = new[]
            {
                new
                {
                    productId = Guid.NewGuid(),
                    quantity = 2,
                    unitPrice = 19.99m
                }
            }
        };

        var response = await _client.PostAsJsonAsync("/orders", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();

        Assert.NotNull(payload);
        Assert.Equal("Created", payload.Status);
        Assert.Equal(request.customerId, payload.CustomerId);
    }

    private sealed record CreateOrderResponse(Guid OrderId, Guid CustomerId, string Status, DateTime CreatedAtUtc);
}
