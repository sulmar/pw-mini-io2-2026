using Gpu;

namespace Gpu.Tests;

public class GpuTests
{
    [Fact]
    public void Constructor_WhenGpuIsCreated_SetsStatusToIdle()
    {
        // Arrange

        // Act
        var gpu = new Gpu();

        // Assert
        Assert.Equal(GpuStatus.Idle, gpu.Status);
    }

    [Fact]
    public void Start_WhenGpuIsIdle_SetsStatusToIsRunning()
    {
        // Arrange
        var gpu = new Gpu();

        // Act
        gpu.Start();

        // Assert
        Assert.Equal(GpuStatus.IsRunning, gpu.Status);
    }

    [Fact]
    public void Start_WhenGpuIsAlreadyRunning_ThrowsInvalidOperationExceptionWithExpectedMessage()
    {
        // Arrange
        var gpu = new Gpu();
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
        var gpu = new Gpu();
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
        var gpu = new Gpu();

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => gpu.Stop());

        // Assert
        Assert.Equal("GPU is not running", exception.Message);
    }

    [Fact]
    public void TotalRunningTime_WhenStoppedAfterOneHourOfRunning_EqualsOneHour()
    {
        // Arrange
        var clock = new FakeTimeProvider(new DateTimeOffset(2025, 1, 1, 12, 0, 0, TimeSpan.Zero));
        var gpu = new Gpu(clock);

        // Act
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(1));
        gpu.Stop();

        // Assert
        Assert.Equal(TimeSpan.FromHours(1), gpu.TotalRunningTime);
    }

    [Fact]
    public void TotalRunningTime_WhenStoppedAfterFiveHoursOfRunning_EqualsFiveHours()
    {
        // Arrange
        var clock = new FakeTimeProvider(new DateTimeOffset(2025, 6, 1, 0, 0, 0, TimeSpan.Zero));
        var gpu = new Gpu(clock);

        // Act
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(5));
        gpu.Stop();

        // Assert
        Assert.Equal(TimeSpan.FromHours(5), gpu.TotalRunningTime);
    }

    [Fact]
    public void TotalRunningTime_WhenMultipleSessionsStopped_AccumulatesDurations()
    {
        // Arrange
        var clock = new FakeTimeProvider(new DateTimeOffset(2025, 3, 1, 8, 0, 0, TimeSpan.Zero));
        var gpu = new Gpu(clock);

        // Act
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(2));
        gpu.Stop();
        gpu.Start();
        clock.Advance(TimeSpan.FromHours(3));
        gpu.Stop();

        // Assert
        Assert.Equal(TimeSpan.FromHours(5), gpu.TotalRunningTime);
    }
}
