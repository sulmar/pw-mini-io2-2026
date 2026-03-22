namespace Gpu;

public interface IGpuCostCalculator
{
    decimal Calculate(decimal hourlyRate, TimeSpan duration);
}
