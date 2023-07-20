using System.Net.Http.Json;

namespace BlazorApp1.Gateways;

public class WebApiClient
{
    readonly HttpClient _httpClient;

    public WebApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAnonymousMessage() =>
        await _httpClient.GetFromJsonAsync<string>("anonymous");

    public async Task<string> GetAuthorizedUserMessage() =>
        await _httpClient.GetFromJsonAsync<string>("authorizeduser");
}
