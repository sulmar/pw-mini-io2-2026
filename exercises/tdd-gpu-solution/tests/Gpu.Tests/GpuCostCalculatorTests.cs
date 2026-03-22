using Gpu;

namespace Gpu.Tests;

public class GpuCostCalculatorTests
{
    [Fact]
    public void Calculate_WhenHourlyRateIsNotPositive_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var calculator = new GpuCostCalculator();
        var duration = TimeSpan.FromHours(1);

        // Act
        var exZero = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(0m, duration));
        var exNegative = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(-1m, duration));

        // Assert
        Assert.Equal("hourlyRate", exZero.ParamName);
        Assert.Equal("hourlyRate", exNegative.ParamName);
    }

    [Fact]
    public void Calculate_WhenDurationIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var calculator = new GpuCostCalculator();

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(10m, TimeSpan.FromHours(-1)));

        // Assert
        Assert.Equal("duration", ex.ParamName);
    }

    [Theory]
    [InlineData(1, 25, 25)]
    [InlineData(5, 4, 20)]
    [InlineData(10, 3, 30)]
    public void Calculate_WhenDurationIsTypicalHours_ReturnsHourlyRateTimesHours(
        double hours,
        decimal hourlyRate,
        decimal expectedCost)
    {
        // Arrange
        var calculator = new GpuCostCalculator();
        var duration = TimeSpan.FromHours(hours);

        // Act
        var cost = calculator.Calculate(hourlyRate, duration);

        // Assert
        Assert.Equal(expectedCost, cost);
    }

    [Fact]
    public void Calculate_WhenMaxCostIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var calculator = new GpuCostCalculator();

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            calculator.Calculate(10m, TimeSpan.FromHours(1), -1m));

        // Assert
        Assert.Equal("maxCost", ex.ParamName);
    }

    [Theory]
    [InlineData(100, 1, 50, 50)] // surowy koszt 100, limit 50
    [InlineData(10, 1, 50, 10)] // surowy 10, limit 50
    [InlineData(10, 10, 50, 50)] // surowy 100, limit 50
    public void Calculate_WhenMaxCostIsSet_ReturnsLesserOfRawCostAndMax(
        decimal hourlyRate,
        double hours,
        decimal maxCost,
        decimal expected)
    {
        // Arrange
        var calculator = new GpuCostCalculator();
        var duration = TimeSpan.FromHours(hours);

        // Act
        var cost = calculator.Calculate(hourlyRate, duration, maxCost);

        // Assert
        Assert.Equal(expected, cost);
    }
}
