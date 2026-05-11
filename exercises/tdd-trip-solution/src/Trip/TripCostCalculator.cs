namespace Trip;

// Konkretny komponent: koszt = dystans × stawka; może być opakowany przez dekoratory limitu kosztu.
public sealed class TripCostCalculator : ITripCostCalculator
{
    public decimal Calculate(decimal distanceKm, decimal ratePerKm)
    {
        if (distanceKm < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distanceKm));
        }

        if (ratePerKm <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ratePerKm));
        }

        return distanceKm * ratePerKm;
    }
}
