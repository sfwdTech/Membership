namespace Membership.Abstractions.Interfaces.Services;
public interface IAccessTokenService
{
    Task<string> GetNewUserAccessTokenAsync(UserEntity userEntity);
    Task<string> RotateAccessTokenAsync(string accessTokenToRotate);
}
