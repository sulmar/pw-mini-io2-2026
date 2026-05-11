using Gpu;

namespace Gpu.Tests;

public class GpuBillingEndToEndTests
{
    [Fact]
    public void Billing_WhenStartStopTwoHoursAt15PerHour_UncappedCostIs30_CappedAt20Returns20()
    {
        // Arrange
        var clock = new FakeTimeProvider(new DateTimeOffset(2025, 2, 15, 9, 0, 0, TimeSpan.Zero));
        var gpu = new Gpu(clock);
        const decimal hourlyRate = 15m;

        // Act — Start → upływ czasu → Stop (jak w tdd-gpu.md)
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(2));
        gpu.Stop();

        IGpuCostCalculator baseCalculator = new GpuCostCalculator();
        var uncappedCost = baseCalculator.Calculate(hourlyRate, gpu.TotalRunningTime);

        // Ten sam czas pracy, ale koszt przez dekorator (limit 20 zamiast pełnego 30).
        IGpuCostCalculator cappedCalculator = new MaxCostGpuCostCalculatorDecorator(new GpuCostCalculator(), 20m);
        var cappedCost = cappedCalculator.Calculate(hourlyRate, gpu.TotalRunningTime);

        // Assert
        Assert.Equal(30m, uncappedCost);
        Assert.Equal(20m, cappedCost);
    }
}
