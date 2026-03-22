using Gpu;

namespace Gpu.Tests;

public class MaxCostGpuCostCalculatorDecoratorTests
{
    [Fact]
    public void Constructor_WhenMaxCostIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        IGpuCostCalculator inner = new GpuCostCalculator();

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new MaxCostGpuCostCalculatorDecorator(inner, -1m));

        // Assert
        Assert.Equal("maxCost", ex.ParamName);
    }

    [Theory]
    [InlineData(100, 1, 50, 50)]
    [InlineData(10, 1, 50, 10)]
    [InlineData(10, 10, 50, 50)]
    public void Calculate_WhenRawCostVaries_ReturnsLesserOfInnerCostAndMax(
        decimal hourlyRate,
        double hours,
        decimal maxCost,
        decimal expected)
    {
        // Arrange
        IGpuCostCalculator inner = new GpuCostCalculator();
        var calculator = new MaxCostGpuCostCalculatorDecorator(inner, maxCost);
        var duration = TimeSpan.FromHours(hours);

        // Act
        var cost = calculator.Calculate(hourlyRate, duration);

        // Assert
        Assert.Equal(expected, cost);
    }
}
