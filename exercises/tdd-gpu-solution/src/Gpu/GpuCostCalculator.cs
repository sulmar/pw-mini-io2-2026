namespace Gpu;

public sealed class GpuCostCalculator : IGpuCostCalculator
{
    public decimal Calculate(decimal hourlyRate, TimeSpan duration)
    {
        if (hourlyRate <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(hourlyRate));
        }

        if (duration < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(duration));
        }

        return hourlyRate * (decimal)duration.TotalHours;
    }
}
