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

    [Fact]
    public void Calculate_WhenHourlyRateIs25AndDurationIsOneHour_Returns25()
    {
        // Arrange
        var calculator = new GpuCostCalculator();

        // Act
        var cost = calculator.Calculate(25m, TimeSpan.FromHours(1));

        // Assert
        Assert.Equal(25m, cost);
    }

    [Fact]
    public void Calculate_WhenHourlyRateIs4AndDurationIsFiveHours_Returns20()
    {
        // Arrange
        var calculator = new GpuCostCalculator();

        // Act
        var cost = calculator.Calculate(4m, TimeSpan.FromHours(5));

        // Assert
        Assert.Equal(20m, cost);
    }
}
