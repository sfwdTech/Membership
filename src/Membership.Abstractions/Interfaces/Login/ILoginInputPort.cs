namespace Membership.Abstractions.Interfaces.Login;
public interface ILoginInputPort
{
    Task LoginAsync(UserCredentialsDTO userCredentials);
}
