using Trip;

namespace Trip.Tests;

public class TripCostCalculatorTests
{
    [Fact]
    public void Calculate_WhenRatePerKmIsNotPositive_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var calculator = new TripCostCalculator();

        // Act
        var exZero = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(100m, 0m));
        var exNegative = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(100m, -1m));

        // Assert
        Assert.Equal("ratePerKm", exZero.ParamName);
        Assert.Equal("ratePerKm", exNegative.ParamName);
    }

    [Fact]
    public void Calculate_WhenDistanceKmIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var calculator = new TripCostCalculator();

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(-1m, 2m));

        // Assert
        Assert.Equal("distanceKm", ex.ParamName);
    }

    [Theory]
    [InlineData(100, 2, 200)]
    [InlineData(500, 1, 500)]
    [InlineData(1000, 3, 3000)]
    public void Calculate_WhenTypicalDistances_ReturnsDistanceTimesRate(
        decimal distanceKm,
        decimal ratePerKm,
        decimal expectedCost)
    {
        // Arrange
        var calculator = new TripCostCalculator();

        // Act
        var cost = calculator.Calculate(distanceKm, ratePerKm);

        // Assert
        Assert.Equal(expectedCost, cost);
    }
}
