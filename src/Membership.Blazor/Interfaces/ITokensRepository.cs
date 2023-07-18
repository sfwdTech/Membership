namespace Membership.Blazor.Interfaces;
internal interface ITokensRepository
{
    Task SaveTokensAsync(UserTokensDTO userTokensDTO);
    Task<UserTokensDTO> GetTokensAsync();
    Task RemoveTokensAsync();
}
