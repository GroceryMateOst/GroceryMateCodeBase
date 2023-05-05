using GeoCoordinatePortable;

namespace grocery_mate_backend.Service;

public class DistanceCalculationService
{
    public static double CalculateDistance(double sourceLat, double sourceLon, double targetLat, double targetLon)
    {
        return new GeoCoordinate(sourceLat, sourceLon).GetDistanceTo(new GeoCoordinate(targetLat, targetLon));
    }
}