namespace Membership.Core.Controllers;
internal class LogoutController : ILogoutController
{
    readonly ILogoutInputPort _logoutInputPort;

    public async Task LogoutAsycn(UserTokensDTO userTokens) =>
        await _logoutInputPort.LogoutAsync(userTokens);
    
}
