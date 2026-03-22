namespace Gpu;

public class Gpu
{
    private readonly decimal _hourlyRate;
    private readonly TimeProvider _time;
    private DateTimeOffset? _runStartedAt;
    private TimeSpan _accumulatedRunning;

    public Gpu(decimal hourlyRate, TimeProvider? time = null)
    {
        if (hourlyRate <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(hourlyRate));
        }

        _hourlyRate = hourlyRate;
        _time = time ?? TimeProvider.System;
    }

    public GpuStatus Status { get; private set; } = GpuStatus.Idle;

    public void Start()
    {
        if (Status == GpuStatus.IsRunning)
        {
            throw new InvalidOperationException("GPU already running.");
        }

        _runStartedAt = _time.GetUtcNow();
        Status = GpuStatus.IsRunning;
    }

    public void Stop()
    {
        if (Status == GpuStatus.Idle)
        {
            throw new InvalidOperationException("GPU is not running");
        }

        _accumulatedRunning += _time.GetUtcNow() - _runStartedAt!.Value;
        _runStartedAt = null;
        Status = GpuStatus.Idle;
    }

    public decimal GetTotalCost() => _hourlyRate * (decimal)_accumulatedRunning.TotalHours;
}
