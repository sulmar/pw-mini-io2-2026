using Trip;

namespace Trip.Tests;

public class TripBillingEndToEndTests
{
    [Fact]
    public void Billing_WhenRoute100KmAt2PerKm_UncappedCostIs200_CappedAt150Returns150()
    {
        // Arrange
        var matrix = new[,]
        {
            { 0, 100 },
            { 100, 0 }
        };
        var planner = new RoutePlanner(matrix);
        const decimal ratePerKm = 2m;

        // Act — dystans z macierzy → koszt (tdd-trip.md: A → B → długość → koszt)
        var distanceKm = (decimal)planner.GetDistanceBetween(0, 1);

        ITripCostCalculator baseCalculator = new TripCostCalculator();
        var uncappedCost = baseCalculator.Calculate(distanceKm, ratePerKm);

        ITripCostCalculator cappedCalculator = new MaxCostTripCostCalculatorDecorator(new TripCostCalculator(), 150m);
        var cappedCost = cappedCalculator.Calculate(distanceKm, ratePerKm);

        // Assert
        Assert.Equal(100m, distanceKm);
        Assert.Equal(200m, uncappedCost);
        Assert.Equal(150m, cappedCost);
    }
}
