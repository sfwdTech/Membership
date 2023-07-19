using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Membership.Blazor.AuthenticationStateProviders;
internal class JWTAuthencticationStateProvider : AuthenticationStateProvider,
    IAuthenticationStateProvider
{
    readonly IUserWebApiGateway _userWebApiGateway;
    readonly ITokensRepository _tokensRepository;

    public JWTAuthencticationStateProvider(IUserWebApiGateway userWebApiGateway,
               ITokensRepository tokensRepository)
    {
        _userWebApiGateway = userWebApiGateway;
        _tokensRepository = tokensRepository;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

        var storedTokens = await GetUserTokensAsync();
        if(storedTokens != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(storedTokens.AccessToken);
            claimsIdentity = new ClaimsIdentity(token.Claims, "Bearer");
        }

        return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
    }

    public async Task<UserTokensDTO> GetUserTokensAsync()
    {
        UserTokensDTO storedTokens = await _tokensRepository.GetTokensAsync();
        if(storedTokens != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(storedTokens.AccessToken);
            if(token.ValidTo <= DateTime.UtcNow)
            {
                try
                {
                    var newTokens = await _userWebApiGateway.RefreshTokensAsync(storedTokens);
                    await LoginAsync(newTokens);
                    storedTokens = newTokens;
                    Console.WriteLine("Tokens refreshed");
                }
                catch (Exception ex)
                {
                    storedTokens = default;
                    Console.WriteLine(ex.Message);
                    await LogoutAsync();
                }
            }
        }
        return storedTokens;
    }

    public async Task LoginAsync(UserTokensDTO userTokensDTO)
    {
        await _tokensRepository.SaveTokensAsync(userTokensDTO);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await _tokensRepository.RemoveTokensAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
