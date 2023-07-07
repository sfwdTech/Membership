namespace Membership.Core.Controllers;
internal class LoginController : ILoginController
{
    readonly ILoginInputPort _loginInputPort;
    readonly ILoginOutputPort _loginOutputPort;

    public LoginController(ILoginInputPort loginInputPort, ILoginOutputPort loginOutputPort)
    {
        _loginInputPort = loginInputPort;
        _loginOutputPort = loginOutputPort;
    }

    public async Task<UserTokensDTO> LogingAsync(UserCredentialsDTO userCredentials)
    {
        await _loginInputPort.LoginAsync(userCredentials);

        return _loginOutputPort.UserTokens;
    }
}
