namespace Membership.Blazor.Interfaces;
internal interface IUserWebApiGateway
{
    Task RegisterUserAsync(UserDTO userDTO);
    Task<UserTokensDTO> LoginAsync(UserCredentialsDTO userCredentialsDTO);
    Task<UserTokensDTO> RefreshTokensAsync(UserTokensDTO userTokensDTO);
    Task LogoutAsync(UserTokensDTO userTokensDTO);
}
