namespace Membership.Abstractions.Interfaces.RefreshToken;
public interface IRefreshTokenInputPort
{
    Task RefreshTokenAsync(UserTokensDTO userTokens);
}
