using Newtonsoft.Json;

namespace dj_service;

public class SearchAccess(IConfiguration config) : ISearchAccess
{
    private static readonly string GeniusBaseUrl = "https://api.genius.com/search?q=";
    private readonly string _geniusToken = Environment.GetEnvironmentVariable("Genius_Access_Token") ?? config.GetValue<string>("Genius_Access_Token");

    public async Task<GeniusSearchResponse> GeniusSearchAsync(string query)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_geniusToken}");
        var response = await client.GetAsync(GeniusBaseUrl + Uri.EscapeDataString(query));
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GeniusSearchResponse>(content);
    }
}
