namespace Membership.Abstractions.Interfaces.Services;
public interface IRefreshTokenService
{
    Task<string> GetRefreshTokenForAccessTokenAsync(string accessToken);
    Task DeleteRefreshTokenAsync(string refreshToken);

    Task ThrowIfUnableToRotateRefreshToken(string refreshToken, string accessToken);
}
