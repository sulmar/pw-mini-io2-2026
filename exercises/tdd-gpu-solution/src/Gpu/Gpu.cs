namespace Gpu;

public class Gpu
{
    public GpuStatus Status { get; private set; } = GpuStatus.Idle;

    public void Start()
    {
        if (Status == GpuStatus.IsRunning)
        {
            throw new InvalidOperationException("GPU already running.");
        }

        Status = GpuStatus.IsRunning;
    }

    public void Stop()
    {
        if (Status == GpuStatus.Idle)
        {
            throw new InvalidOperationException("GPU is not running");
        }

        Status = GpuStatus.Idle;
    }
}
