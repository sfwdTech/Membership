namespace Membership.Blazor.Services;
internal class AuthorizeService : IAuthorizeService
{
    readonly IOptions<AppOptions> _options;
    readonly IOAuthStateService _stateService;
    readonly IOAuthService _oAuthService;
    readonly NavigationManager _navigationManager;

    public AuthorizeService(IOptions<AppOptions> options, IOAuthStateService stateService,
        IOAuthService oAuthService, NavigationManager navigationManager)
    {
        _options = options;
        _stateService = stateService;
        _oAuthService = oAuthService;
        _navigationManager = navigationManager;
    }

    public ExternalIDPInfo[] IDPs => _options.Value.IDPs;

    public async Task AuthorizeAsync(string providerId, ScopeAction action, string returnUri)
    {
        var stateInfo = 
            new StateInfo(_oAuthService.GetState(), _oAuthService.GetCodeVerifier(),
            _oAuthService.GetNonce(), $"{action}_{providerId}", returnUri);

        var requestData = new AuthorizeRequestInfo(
            _options.Value.AuthorizationEndpoint,
            _options.Value.ClientId,
            _options.Value.RedirectUri,
            stateInfo.State,
            stateInfo.Scope,
            _oAuthService.GetHash256CodeChallenge(stateInfo.CodeVerifier),
            _oAuthService.CodeChallengeMethodSha256,
            stateInfo.Nonce);

        await _stateService.SetAsync(stateInfo.State, stateInfo);

        _navigationManager.NavigateTo(_oAuthService.BuildAuthorizeRequestUri(requestData));
    }
}
