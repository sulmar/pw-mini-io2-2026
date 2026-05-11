namespace Gpu;

// Decorator: opakowuje dowolny IGpuCostCalculator (_inner), deleguje obliczenie,
// a następnie ogranicza wynik do maksymalnego kosztu (bez zmiany klasy GpuCostCalculator).
public sealed class MaxCostGpuCostCalculatorDecorator : IGpuCostCalculator
{
    private readonly IGpuCostCalculator _inner;
    private readonly decimal _maxCost;

    public MaxCostGpuCostCalculatorDecorator(IGpuCostCalculator inner, decimal maxCost)
    {
        if (maxCost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxCost));
        }

        _inner = inner;
        _maxCost = maxCost;
    }

    public decimal Calculate(decimal hourlyRate, TimeSpan duration)
    {
        // Najpierw zachowanie opakowywanego obiektu, potem dodatkowa odpowiedzialność dekoratora.
        var cost = _inner.Calculate(hourlyRate, duration);
        return Math.Min(cost, _maxCost);
    }
}
