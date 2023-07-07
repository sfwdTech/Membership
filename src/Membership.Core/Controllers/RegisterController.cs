namespace Membership.Core.Controllers;
public class RegisterController : IRegisterController
{
    readonly IRegisterInputPort _inputPort;

    public RegisterController(IRegisterInputPort inputPort)
    {
        _inputPort = inputPort;
    }

    public Task RegisterAsync(UserDTO user) => 
        _inputPort.RegisterAsync(user);
    
}
