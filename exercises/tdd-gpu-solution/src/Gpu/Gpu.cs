namespace Gpu;

public class Gpu
{
    public GpuStatus Status { get; private set; } = GpuStatus.Idle;

    public void Start()
    {
        Status = GpuStatus.IsRunning;
    }
}
