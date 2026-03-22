namespace Trip;

// Można rozważyć wyniesienie int[,] _distances do osobnej klasy (np. DistanceMatrix).
// Jeśli nie planujesz drugiego miejsca używającego tej samej struktury ani innej reprezentacji
// (np. rzadka macierz), YAGNI mówi: zostaw w RoutePlanner, dopóki nie poczujesz tarcia.
public sealed class RoutePlanner
{
    private readonly int[,] _distances;

    public RoutePlanner(int[,] distances)
    {
        _distances = distances ?? throw new ArgumentNullException(nameof(distances));
        if (distances.GetLength(0) != distances.GetLength(1))
        {
            throw new ArgumentException("Distance matrix must be square.", nameof(distances));
        }
    }

    public int GetDistanceBetween(int fromCityIndex, int toCityIndex)
    {
        if (fromCityIndex == toCityIndex)
        {
            throw new ArgumentException("Start and destination must be different cities.", nameof(toCityIndex));
        }

        ThrowIfCityNotInMatrix(fromCityIndex, nameof(fromCityIndex));
        ThrowIfCityNotInMatrix(toCityIndex, nameof(toCityIndex));

        return _distances[fromCityIndex, toCityIndex];
    }

    private void ThrowIfCityNotInMatrix(int cityIndex, string paramName)
    {
        var n = _distances.GetLength(0);
        if (cityIndex < 0 || cityIndex >= n)
        {
            throw new ArgumentException("City is not present in the distance data.", paramName);
        }
    }
}
