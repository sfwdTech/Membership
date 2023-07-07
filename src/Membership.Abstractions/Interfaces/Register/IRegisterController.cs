namespace Membership.Abstractions.Interfaces.Register;
public interface IRegisterController
{
    Task RegisterAsync(UserDTO user);
}
