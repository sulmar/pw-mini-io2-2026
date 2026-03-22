namespace Gpu;

public sealed class Gpu
{
    private GpuStatus _status = GpuStatus.Idle;

    public GpuStatus Status => _status;

    public void Start()
    {
        _status = GpuStatus.IsRunning;
    }
}
