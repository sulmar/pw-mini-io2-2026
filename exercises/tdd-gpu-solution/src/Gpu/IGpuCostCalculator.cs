namespace Gpu;

// Wspólny kontrakt dla kalkulatora bazowego i dekoratorów (wzorzec Decorator —
// oba typy są wymienne przez to samo API).
public interface IGpuCostCalculator
{
    decimal Calculate(decimal hourlyRate, TimeSpan duration);
}
