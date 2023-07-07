namespace Membership.Core.Controllers;
internal class RefreshTokenController : IRefreshTokenController
{
    readonly IRefreshTokenInputPort _inputPort;
    readonly IRefreshTokenOutputPort _outputPort;

    public RefreshTokenController(IRefreshTokenInputPort inputPort, IRefreshTokenOutputPort outputPort)
    {
        _inputPort = inputPort;
        _outputPort = outputPort;
    }

    public async Task<UserTokensDTO> RefreshTokenAsync(UserTokensDTO userTokens)
    {
        await _inputPort.RefreshTokenAsync(userTokens);
        return _outputPort.UserTokens;
    }
}
