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
}
