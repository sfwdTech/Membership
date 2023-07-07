using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Core.Presenters;
public class RefreshTokenPresenter : IRefreshTokenOutputPort
{
    readonly IAccessTokenService _accessTokenService;
    readonly IRefreshTokenService _refreshTokenService;

    public RefreshTokenPresenter(
               IAccessTokenService accessTokenService,
                      IRefreshTokenService refreshTokenService)
    {
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public UserTokensDTO UserTokens { get; private set; }

    public async Task HandleAccessTokenAsync(string accessToken)
    {
        string accessTokenRotate = await _accessTokenService
            .RotateAccessTokenAsync(accessToken);

        string refreshTokenRotate = await _refreshTokenService
            .GetRefreshTokenForAccessTokenAsync(accessTokenRotate);

        UserTokens = new UserTokensDTO(accessTokenRotate, refreshTokenRotate);
    }
}
