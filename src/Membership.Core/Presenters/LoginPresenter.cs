using Membership.Abstractions.Entities;

namespace Membership.Core.Presenters;
public class LoginPresenter : ILoginOutputPort
{
    readonly IAccessTokenService _accessTokenService;
    readonly IRefreshTokenService _refreshTokenService;

    public LoginPresenter(IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
    {
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public UserTokensDTO UserTokens { get; private set; }

    public async Task HandleUserEntityAsync(UserEntity userEntity)
    {
        string accessToken = await _accessTokenService.GetNewUserAccessTokenAsync(userEntity);
        string refreshToken = await _refreshTokenService.GetRefreshTokenForAccessTokenAsync(accessToken);

        UserTokens = new UserTokensDTO(accessToken, refreshToken);
    }
}
