namespace Trip;

// Wspólny kontrakt dla kalkulatora bazowego i dekoratorów (wzorzec Decorator).
public interface ITripCostCalculator
{
    decimal Calculate(decimal distanceKm, decimal ratePerKm);
}
