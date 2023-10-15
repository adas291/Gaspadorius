using System.Runtime.InteropServices;

namespace Gaspadorius.Models;

public static class CityMapper
{
    private static readonly Dictionary<string, int> cityDictionary = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Kaunas", 1 },
        { "Vilnius", 2 },
        { "Klaipeda", 3 },
        // Add more cities and their IDs as needed
    };

    public static int GetCityId(string cityName)
    {
        if (cityDictionary.TryGetValue(cityName, out int cityId))
        {
            return cityId;
        }

        // Handle the case where the city name is not found
        return -1; // or another default value
    }
}
