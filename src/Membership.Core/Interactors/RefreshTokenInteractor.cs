namespace Membership.Core.Interactors;
internal class RefreshTokenInteractor : IRefreshTokenInputPort
{
    readonly IRefreshTokenService _refreshTokenService;
    readonly IRefreshTokenOutputPort _refreshTokenOutputPort;

    public RefreshTokenInteractor(
               IRefreshTokenService refreshTokenService,
                      IRefreshTokenOutputPort refreshTokenOutputPort)
    {
        _refreshTokenService = refreshTokenService;
        _refreshTokenOutputPort = refreshTokenOutputPort;
    }

    public async Task RefreshTokenAsync(UserTokensDTO userTokens)
    {
        await _refreshTokenService
            .ThrowIfUnableToRotateRefreshToken(userTokens.RefreshToken,
            userTokens.AccessToken);

        await _refreshTokenOutputPort.HandleAccessTokenAsync(userTokens.AccessToken);
    }
}
