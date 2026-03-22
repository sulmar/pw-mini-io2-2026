using Gpu;

namespace Gpu.Tests;

public class GpuStartTests
{
    [Fact]
    public void Start_WhenIdle_SetsStatusToIsRunning()
    {
        // Arrange
        var gpu = new Gpu();

        // Act
        gpu.Start();

        // Assert
        Assert.Equal(GpuStatus.IsRunning, gpu.Status);
    }
}
