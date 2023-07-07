namespace Membership.Abstractions.Interfaces.RefreshToken;
public interface IRefreshTokenOutputPort
{
    UserTokensDTO UserTokens { get; }
    Task HandleAccessTokenAsync(string accessToken);
}
