namespace Gpu;

public sealed class GpuCostCalculator
{
    public decimal Calculate(decimal hourlyRate, TimeSpan duration, decimal? maxCost = null)
    {
        if (hourlyRate <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(hourlyRate));
        }

        if (duration < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(duration));
        }

        if (maxCost is < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxCost));
        }

        var cost = hourlyRate * (decimal)duration.TotalHours;
        return maxCost is null ? cost : Math.Min(cost, maxCost.Value);
    }
}
