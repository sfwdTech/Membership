namespace Membership.Blazor.Interfaces;
internal interface IAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task LoginAsync(UserTokensDTO userTokensDTO);
    Task LogoutAsync();
    Task<UserTokensDTO> GetUserTokensAsync();
}
