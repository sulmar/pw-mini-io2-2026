using Gpu;

namespace Gpu.Tests;

public class GpuTests
{
    [Fact]
    public void Constructor_WhenGpuIsCreated_SetsStatusToIdle()
    {
        // Arrange

        // Act
        var gpu = new Gpu(1m);

        // Assert
        Assert.Equal(GpuStatus.Idle, gpu.Status);
    }

    [Fact]
    public void Start_WhenGpuIsIdle_SetsStatusToIsRunning()
    {
        // Arrange
        var gpu = new Gpu(1m);

        // Act
        gpu.Start();

        // Assert
        Assert.Equal(GpuStatus.IsRunning, gpu.Status);
    }

    [Fact]
    public void Start_WhenGpuIsAlreadyRunning_ThrowsInvalidOperationExceptionWithExpectedMessage()
    {
        // Arrange
        var gpu = new Gpu(1m);
        gpu.Start();

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => gpu.Start());

        // Assert
        Assert.Equal("GPU already running.", exception.Message);
    }

    [Fact]
    public void Stop_WhenGpuIsRunning_SetsStatusToIdle()
    {
        // Arrange
        var gpu = new Gpu(1m);
        gpu.Start();

        // Act
        gpu.Stop();

        // Assert
        Assert.Equal(GpuStatus.Idle, gpu.Status);
    }

    [Fact]
    public void Stop_WhenGpuIsIdle_ThrowsInvalidOperationExceptionWithExpectedMessage()
    {
        // Arrange
        var gpu = new Gpu(1m);

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => gpu.Stop());

        // Assert
        Assert.Equal("GPU is not running", exception.Message);
    }

    [Fact]
    public void Constructor_WhenHourlyRateIsNotPositive_ThrowsArgumentOutOfRangeException()
    {
        // Arrange

        // Act
        var exWhenZero = Assert.Throws<ArgumentOutOfRangeException>(() => new Gpu(0m));
        var exWhenNegative = Assert.Throws<ArgumentOutOfRangeException>(() => new Gpu(-10m));

        // Assert
        Assert.Equal("hourlyRate", exWhenZero.ParamName);
        Assert.Equal("hourlyRate", exWhenNegative.ParamName);
    }

    [Fact]
    public void GetTotalCost_WhenStoppedAfterOneHourOfRunning_ReturnsHourlyRateTimesElapsedHours()
    {
        // Arrange
        var clock = new FakeTimeProvider(new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero));
        var gpu = new Gpu(25m, clock);

        // Act
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(1));
        gpu.Stop();

        // Assert
        Assert.Equal(25m, gpu.GetTotalCost());
    }

    [Fact]
    public void GetTotalCost_WhenStoppedAfterFiveHoursOfRunning_ReturnsHourlyRateTimesFive()
    {
        // Arrange
        var clock = new FakeTimeProvider(new DateTimeOffset(2025, 6, 1, 0, 0, 0, TimeSpan.Zero));
        var gpu = new Gpu(4m, clock);

        // Act
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(5));
        gpu.Stop();

        // Assert
        Assert.Equal(20m, gpu.GetTotalCost());
    }
}
