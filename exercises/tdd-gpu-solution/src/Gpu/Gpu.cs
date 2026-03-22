namespace Gpu;

// Można rozważyć wzorzec Stan (State) do enkapsulacji przejść i reguł dla stanów karty (Idle, IsRunning, …).
// Jeśli nie planujesz rozgałęzionej logiki zależnej od stanu ani drugiego miejsca z tą samą maszyną stanów,
// YAGNI mówi: zostaw proste przejścia w Gpu, dopóki nie poczujesz tarcia.
//
// Nie używaj DateTime.UtcNow / DateTime.Now w logice czasu — blokuje to sensowne testy jednostkowe (czas jest niedeterministyczny).
// Lepiej wstrzyknąć abstrakcję: wbudowany TimeProvider albo własny interfejs „zegara”.
public class Gpu
{
    private readonly TimeProvider _time;
    private DateTimeOffset? _runStartedAt;
    private TimeSpan _accumulatedRunning;

    public Gpu(TimeProvider? time = null)
    {
        _time = time ?? TimeProvider.System;
    }

    public GpuStatus Status { get; private set; } = GpuStatus.Idle;

    public TimeSpan TotalRunningTime => _accumulatedRunning;

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
}
