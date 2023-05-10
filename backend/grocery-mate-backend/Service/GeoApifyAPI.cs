using System.Web;
using GeoJSON.Net.Feature;
using grocery_mate_backend.Models;
using Newtonsoft.Json;

namespace grocery_mate_backend.Service;

public static class GeoApifyApi
{
    private const string BaseUrl = "https://api.geoapify.com/v1/geocode/search?";
    private const string ApiKey = "71929fcccf5249b8937bc3a51ca27447";

    public static async Task<(double lon, double lat)> GetCoordinates(
        string street,
        string houseNr,
        string city,
        int postcode,
        string state)
    {
        using HttpClient client = new();
        var resp = await client.GetAsync(BaseUrl + $"housenumber={HttpUtility.UrlEncode(houseNr)}&" +
                                         $"street={HttpUtility.UrlEncode(street)}&" +
                                         $"postcode={postcode}&" +
                                         $"city={HttpUtility.UrlEncode(city)}&" +
                                         $"state={HttpUtility.UrlEncode(state)}&" +
                                         "country=Switzerland&" +
                                         "bias=countrycode:de,at,ch&" +
                                         "format=geojson&" +
                                         $"apiKey={ApiKey}");
        resp.EnsureSuccessStatusCode();

        var geoFeatures = JsonConvert.DeserializeObject<FeatureCollection>(await resp.Content.ReadAsStringAsync());
        var geoFeature = geoFeatures?.Features.FirstOrDefault();
        if (geoFeature == null)
        {
            throw new InvalidOperationException("No location found matching given data");
        }

        var properties = geoFeature.Properties;
        return (Convert.ToDouble(properties["lon"]), Convert.ToDouble(properties["lat"]));
    }

    public static async Task<ZipCodeResponseDto> GetCityName(int zipCode)
    {
        using HttpClient client = new();

        var resp = await client.GetAsync(BaseUrl +
                                         $"postcode={zipCode}&" +
                                         $"country=Switzerland" +
                                         $"&filter=countrycode:de,at,ch" +
                                         $"&bias=countrycode:de,at,ch" +
                                         $"&format=geojson" +
                                         $"&apiKey={ApiKey}");
        resp.EnsureSuccessStatusCode();

        var geoFeatures = JsonConvert.DeserializeObject<FeatureCollection>(await resp.Content.ReadAsStringAsync());
        var geoFeature = geoFeatures?.Features.FirstOrDefault();
        if (geoFeature == null)
        {
            throw new InvalidOperationException("No location found matching given data");
        }

        var properties = geoFeature.Properties;
        return new ZipCodeResponseDto(Convert.ToString(properties["city"]) ?? string.Empty,
            Convert.ToString(properties["state"]) ?? string.Empty);
    }

    public static async Task<(double lon, double lat)> GetCoordinatesByZipCode(int zipCode)
    {
        using HttpClient client = new();

        var resp = await client.GetAsync(BaseUrl +
                                         $"postcode={zipCode}&" +
                                         $"country=Switzerland" +
                                         $"&filter=countrycode:de,at,ch" +
                                         $"&bias=countrycode:de,at,ch" +
                                         $"&format=geojson" +
                                         $"&apiKey={ApiKey}");
        resp.EnsureSuccessStatusCode();

        var geoFeatures = JsonConvert.DeserializeObject<FeatureCollection>(await resp.Content.ReadAsStringAsync());
        var geoFeature = geoFeatures?.Features.FirstOrDefault();
        if (geoFeature == null)
        {
            throw new InvalidOperationException("No location found matching given data");
        }

        var properties = geoFeature.Properties;
        return (Convert.ToDouble(properties["lon"]), Convert.ToDouble(properties["lat"]));
    }
}