namespace Membership.Core.Interactors;
internal class LogoutInteractor : ILogoutInputPort
{
    readonly IRefreshTokenService _refreshTokenService;

    public LogoutInteractor(IRefreshTokenService refreshTokenService)
    {
        _refreshTokenService = refreshTokenService;
    }

    public async Task LogoutAsync(UserTokensDTO userTokens)
    {
        await _refreshTokenService.DeleteRefreshTokenAsync(userTokens.RefreshToken);
    }
}
