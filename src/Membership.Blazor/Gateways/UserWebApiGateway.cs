namespace Membership.Blazor.Gateways;
internal class UserWebApiGateway : IUserWebApiGateway
{
    readonly UserEndpointsOptions _options;
    readonly HttpClient _httpClient;

    public UserWebApiGateway(IOptions<UserEndpointsOptions> options, HttpClient httpClient)
    {
        _options = options.Value;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_options.WebApiBaseAddress);
    }

    public async Task<UserTokensDTO> LoginAsync(UserCredentialsDTO userCredentialsDTO)
    {
        var response = await _httpClient
            .PostAsJsonAsync(_options.Login, userCredentialsDTO);
        return await response.Content.ReadFromJsonAsync<UserTokensDTO>();
    }

    public async Task LogoutAsync(UserTokensDTO userTokensDTO)
    {
        
        await _httpClient
            .PostAsJsonAsync(_options.Logout, userTokensDTO);
    }

    public async Task<UserTokensDTO> RefreshTokensAsync(UserTokensDTO userTokensDTO)
    {
        var response = await _httpClient
            .PostAsJsonAsync(_options.RefreshToken, userTokensDTO);
        return await response.Content.ReadFromJsonAsync<UserTokensDTO>();
    }

    public async Task RegisterUserAsync(UserDTO userDTO)
    {
        await _httpClient
            .PostAsJsonAsync(_options.Register, userDTO);
    }
}
