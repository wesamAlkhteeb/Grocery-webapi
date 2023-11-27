

namespace Grocery.Domain.Helper;


public class GeoHelper{
    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double radius = 6371; // Earth's radius in kilometers

        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = radius * c * 1000; // Convert to meters

        return distance;
    }
    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}