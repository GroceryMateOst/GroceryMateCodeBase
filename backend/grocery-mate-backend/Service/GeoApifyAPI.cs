using System.Web;
using GeoJSON.Net.Feature;
using grocery_mate_backend.Models;
using Newtonsoft.Json;

namespace grocery_mate_backend.Service;

public static class GeoApifyApi
{
    private const string BaseUrl = "https://api.geoapify.com/v1/geocode/search?";

    public static async Task<(double lon, double lat)> GetCoordinates(
        string street,
        string houseNr,
        string city,
        int postcode,
        string state,
        string apiKey)
    {
        using HttpClient client = new();
        var resp = await client.GetAsync(BaseUrl + $"?housenumber={HttpUtility.UrlEncode(houseNr)}&" +
                                         $"street={HttpUtility.UrlEncode(street)}&" +
                                         $"postcode={postcode}&" +
                                         $"city={HttpUtility.UrlEncode(city)}&" +
                                         $"state={HttpUtility.UrlEncode(state)}&" +
                                         "country=Switzerland&" +
                                         "bias=countrycode:de,at,   ch&" +
                                         "format=geojson&" +
                                         $"apiKey={apiKey}");
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
    
    public static async Task<ZipResponseDto> GetCityName(int zipCode, string apiKey)
    {
        using HttpClient client = new();

        var resp = await client.GetAsync(BaseUrl + 
                                         $"postcode={zipCode}&" +
                                         $"country=Switzerland" +
                                         $"&filter=countrycode:de,at,ch" +
                                         $"&bias=countrycode:de,at,ch" +
                                         $"&format=geojson" + 
                                         $"&apiKey={apiKey}");
        resp.EnsureSuccessStatusCode();

        var geoFeatures = JsonConvert.DeserializeObject<FeatureCollection>(await resp.Content.ReadAsStringAsync());
        var geoFeature = geoFeatures?.Features.FirstOrDefault();
        if (geoFeature == null)
        {
            throw new InvalidOperationException("No location found matching given data"); 
        }

        var properties = geoFeature.Properties;
        return new ZipResponseDto(Convert.ToString(properties["name"]) ?? string.Empty, 
            Convert.ToString(properties["state"]) ?? string.Empty);
    }
}