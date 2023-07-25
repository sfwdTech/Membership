namespace Membership.Core.Services;
internal class IDPService : IIDPService
{
    readonly HttpClient _httpClient;
    readonly IOAuthService _oAuthService;
    readonly IDPClientInfoOptions _idpClientInfoOptions;
    readonly ILogger<IDPService> _logger;

    public IDPService(HttpClient httpClient,
                      IOAuthService oAuthService,
                      IOptions<IDPClientInfoOptions> idpClientInfoOptions,
                      ILogger<IDPService> logger)
    {
        _httpClient = httpClient;
        _oAuthService = oAuthService;
        _idpClientInfoOptions = idpClientInfoOptions.Value;
        _logger = logger;
    }

    public Task<string> GetAuthorizeRequestUri(string providerId, string state, string codeVerifier, string nonce)
    {
        string Result = null;

        var Info = _idpClientInfoOptions.IDPClients.FirstOrDefault(p => p.ProviderId == providerId);
        if (Info != null)
        {
            string CodeChallenge;
            string CodeChallengeMethod;
            if (Info.SupportsS256CodeChallengeMethod)
            {
                CodeChallenge = _oAuthService.GetHash256CodeChallenge(codeVerifier);
                CodeChallengeMethod = _oAuthService.CodeChallengeMethodSha256;
            }
            else
            {
                CodeChallenge = codeVerifier;
                CodeChallengeMethod = _oAuthService.CodeChallengeMethodPlain;
            }

            AuthorizeRequestInfo RequestInfo = new(Info.AuthorizeEndpoint,
                Info.ClientId, Info.RedirectUri, state, Info.Scope, CodeChallenge,
            CodeChallengeMethod, nonce);

            Result = _oAuthService.BuildAuthorizeRequestUri(RequestInfo);
        }
        return Task.FromResult(Result);
    }

    public async Task<IDPTokens> GetTokesAsync(string providerId, string authorizeCode, string codeVerifier, string nonce)
    {
        IDPTokens tokens = null;
        var info = _idpClientInfoOptions.IDPClients.FirstOrDefault(p => p.ProviderId == providerId);

        var requestBody = _oAuthService.BuildTokenRequestBody(new TokenRequestInfo(
            authorizeCode, info.RedirectUri, info.ClientId, info.Scope,
            codeVerifier, info.ClientSecret));

        var response = await _httpClient.PostAsync(info.TokenEndpoint, requestBody);
        var JsonContentResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

        if (response.IsSuccessStatusCode)
        {
            if (JsonContentResponse.TryGetProperty("id_token", out JsonElement idTokenJson))
            {
                string idTokenToVerify = idTokenJson.ToString();
                // Requiere el paquete NuGet: System.IdentityModel.Tokens.Jwt
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(idTokenToVerify);
                var idTokenNonce = jwtToken.Claims.FirstOrDefault(c => c.Type == "nonce")?.Value;

                if (idTokenNonce != null && idTokenNonce == nonce)
                {
                    tokens = new()
                    {
                        IdToken = idTokenToVerify
                    };

                    if (JsonContentResponse.TryGetProperty("access_token", out JsonElement accessTokenJson))
                        tokens.AccessToken = accessTokenJson.ToString();
                }
            }
        }
        else
        {
            _logger.LogError("{content}", JsonContentResponse.GetRawText());
        }
        return tokens;
    }
}
