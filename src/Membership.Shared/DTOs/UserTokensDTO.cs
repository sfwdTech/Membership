namespace Membership.Shared.DTOs;
public class UserTokensDTO
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public UserTokensDTO(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
