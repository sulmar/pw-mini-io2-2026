using Gpu;

namespace Gpu.Tests;

public class GpuTests
{
    [Fact]
    public void Start_whenGpuIsIdle_setsStatusToIsRunning()
    {
        // Arrange
        var gpu = new Gpu();

        // Act
        gpu.Start();

        // Assert
        Assert.Equal(GpuStatus.IsRunning, gpu.Status);
    }
}
