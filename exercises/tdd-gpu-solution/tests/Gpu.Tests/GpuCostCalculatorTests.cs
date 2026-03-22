using Gpu;

namespace Gpu.Tests;

public class GpuCostCalculatorTests
{
    private readonly GpuCostCalculator _sut = new();

    [Fact]
    public void Calculate_WhenHourlyRateIsNotPositive_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var duration = TimeSpan.FromHours(1);

        // Act
        var exZero = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(0m, duration));
        var exNegative = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(-1m, duration));

        // Assert
        Assert.Equal("hourlyRate", exZero.ParamName);
        Assert.Equal("hourlyRate", exNegative.ParamName);
    }

    [Fact]
    public void Calculate_WhenDurationIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(10m, TimeSpan.FromHours(-1)));

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
        var duration = TimeSpan.FromHours(hours);

        // Act
        var cost = _sut.Calculate(hourlyRate, duration);

        // Assert
        Assert.Equal(expectedCost, cost);
    }
}
