using Trip;

namespace Trip.Tests;

// Testy wzorca Decorator przy limicie kosztu przejazdu.
public class MaxCostTripCostCalculatorDecoratorTests
{
    [Fact]
    public void Constructor_WhenMaxCostIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange — dekorator przyjmuje wewnętrzny ITripCostCalculator
        ITripCostCalculator inner = new TripCostCalculator();

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new MaxCostTripCostCalculatorDecorator(inner, -1m));

        // Assert
        Assert.Equal("maxCost", ex.ParamName);
    }

    [Theory]
    [InlineData(1000, 2, 500, 500)]
    [InlineData(100, 2, 500, 200)]
    [InlineData(400, 1, 300, 300)]
    public void Calculate_WhenRawCostVaries_ReturnsLesserOfInnerCostAndMax(
        decimal distanceKm,
        decimal ratePerKm,
        decimal maxCost,
        decimal expected)
    {
        // Arrange — dekorator owija konkretny kalkulator (inner)
        ITripCostCalculator inner = new TripCostCalculator();
        var calculator = new MaxCostTripCostCalculatorDecorator(inner, maxCost);

        // Act
        var cost = calculator.Calculate(distanceKm, ratePerKm);

        // Assert
        Assert.Equal(expected, cost);
    }
}
