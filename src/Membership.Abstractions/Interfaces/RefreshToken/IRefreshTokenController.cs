namespace Membership.Abstractions.Interfaces.RefreshToken;
public interface IRefreshTokenController
{
    Task<UserTokensDTO> RefreshTokenAsync(UserTokensDTO userTokens);
}
