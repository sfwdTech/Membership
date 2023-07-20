namespace Membership.RefreshTokenService.MemoryCache;
internal class RefreshTokenService : IRefreshTokenService
{
    readonly JwtOptions _jwtOptions;
    readonly IMemoryCache _memoryCache;

    public RefreshTokenService(IOptions<JwtOptions> jwtOptions, IMemoryCache memoryCache)
    {
        _jwtOptions = jwtOptions.Value;
        _memoryCache = memoryCache;
    }

    public Task DeleteRefreshTokenAsync(string refreshToken)
    {
        _memoryCache.Remove(refreshToken);
        return Task.CompletedTask;
    }

    public Task<string> GetRefreshTokenForAccessTokenAsync(string accessToken)
    {
        var refreshToken = GenerateRefreshToken();
        RefreshTokenInfo refreshTokenInfo = new(accessToken,
            DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenExpireInMinutes));

        _memoryCache.Set(refreshToken, refreshTokenInfo,
            DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpireInMinutes + 3));

        return Task.FromResult(refreshToken);
    }

    public Task ThrowIfUnableToRotateRefreshToken(string refreshToken, string accessToken)
    {
        if(_memoryCache.TryGetValue(refreshToken, out RefreshTokenInfo? refreshTokenInfo))
        {
            _memoryCache.Remove(refreshToken);

            if(refreshTokenInfo!.AccessToken != accessToken)
            {
                throw new RefreshTokenCompromisedException();
            }

            if(refreshTokenInfo.RefreshToeknExpiresAt < DateTime.UtcNow)
            {
                throw new RefreshTokenExpiredException();
            }
        }
        else
        {
            throw new RefreshTokenNotFoundException();
        }

        return Task.CompletedTask;
    }

    static string GenerateRefreshToken()
    {
        var randomNumber = new byte[75];
        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
