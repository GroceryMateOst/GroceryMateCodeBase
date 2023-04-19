using System.Web;
using Newtonsoft.Json;

namespace grocery_mate_backend.Service;

public static class GeoApifyApi
{
    private const string BaseUrl = "https://api.geoapify.com/v1/geocode/search?";
    private const string ApiKey = "1ca0f23d53294f628b24b74ce7475d14";

    public static async Task<(double lon, double lat)> GetCoordinates(string street, string houseNr, string city,
        int postcode, string state)
    {
        using (HttpClient client = new())
        {
            var response = await client.GetAsync(BaseUrl + $"?housenumber={HttpUtility.UrlEncode(houseNr)}&" +
                                                 $"street={HttpUtility.UrlEncode(street)}&" +
                                                 $"postcode={postcode}&" +
                                                 $"city={HttpUtility.UrlEncode(city)}&" +
                                                 $"state={HttpUtility.UrlEncode(state)}&" +
                                                 "country=Schweiz&" +
                                                 "bias=countrycode:de,at,ch&" +
                                                 "format=json&" +
                                                 $"apiKey={ApiKey}");
            var addressData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

            var lon = Convert.ToDouble(addressData?.results[0].lon);
            var lat = Convert.ToDouble(addressData?.results[0].lat);

            return (lon, lat);
        }
    }
}