using Trip;

namespace Trip.Tests;

public class RoutePlannerTests
{
    private static int[,] SampleMatrix() =>
        new[,]
        {
            { 0, 100, 250 },
            { 100, 0, 180 },
            { 250, 180, 0 }
        };

    [Fact]
    public void GetDistanceBetween_WhenStartEqualsDestination_ThrowsArgumentException()
    {
        // Arrange
        var planner = new RoutePlanner(SampleMatrix());

        // Act
        var ex = Assert.Throws<ArgumentException>(() => planner.GetDistanceBetween(0, 0));

        // Assert
        Assert.Equal("toCityIndex", ex.ParamName);
    }

    [Fact]
    public void GetDistanceBetween_WhenFromCityIndexIsOutOfRange_ThrowsArgumentException()
    {
        // Arrange
        var planner = new RoutePlanner(SampleMatrix());

        // Act
        var ex = Assert.Throws<ArgumentException>(() => planner.GetDistanceBetween(-1, 1));

        // Assert
        Assert.Equal("fromCityIndex", ex.ParamName);
    }

    [Fact]
    public void GetDistanceBetween_WhenToCityIndexIsOutOfRange_ThrowsArgumentException()
    {
        // Arrange
        var planner = new RoutePlanner(SampleMatrix());

        // Act
        var ex = Assert.Throws<ArgumentException>(() => planner.GetDistanceBetween(0, 5));

        // Assert
        Assert.Equal("toCityIndex", ex.ParamName);
    }

    [Fact]
    public void GetDistanceBetween_WhenRouteIsValid_ReturnsDistanceFromMatrix()
    {
        // Arrange
        var planner = new RoutePlanner(SampleMatrix());

        // Act
        var km = planner.GetDistanceBetween(0, 1);

        // Assert
        Assert.Equal(100, km);
    }
}
