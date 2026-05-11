namespace Trip;

// Decorator: deleguje do wewnętrznego ITripCostCalculator, potem ogranicza wynik do maks. kosztu.
public sealed class MaxCostTripCostCalculatorDecorator : ITripCostCalculator
{
    private readonly ITripCostCalculator _inner;
    private readonly decimal _maxCost;

    public MaxCostTripCostCalculatorDecorator(ITripCostCalculator inner, decimal maxCost)
    {
        if (maxCost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxCost));
        }

        _inner = inner;
        _maxCost = maxCost;
    }

    public decimal Calculate(decimal distanceKm, decimal ratePerKm)
    {
        // Najpierw koszt bazowy, potem limit.
        var cost = _inner.Calculate(distanceKm, ratePerKm);
        return Math.Min(cost, _maxCost);
    }
}
