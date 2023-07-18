namespace Membership.Blazor.Repositories;
internal class TokensRepository : ITokensRepository
{
    const string SessionKey = "asp"; // Authencticaiton Session Provider
    readonly IJSRuntime _jSRuntime;

    public TokensRepository(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task<UserTokensDTO> GetTokensAsync()
    {
        UserTokensDTO storedTokesn = default;
        string value = await _jSRuntime
            .InvokeAsync<string>("sessionStorage.getItem", SessionKey);
        if(value != null)
        {
            string serializedTokens = Encoding.UTF8.GetString(Convert.FromBase64String(value));
            storedTokesn = JsonSerializer.Deserialize<UserTokensDTO>(serializedTokens);
        }
        return storedTokesn;
    }

    public async Task RemoveTokensAsync()
    {
        await _jSRuntime.InvokeVoidAsync("sessionStorage.removeItem", SessionKey);
    }

    public async Task SaveTokensAsync(UserTokensDTO userTokensDTO)
    {
        string serializedTokens = JsonSerializer.Serialize(userTokensDTO);
        string value = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedTokens));
        await _jSRuntime.InvokeVoidAsync("sessionStorage.setItem", SessionKey, value);
    }
}
