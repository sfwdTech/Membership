namespace Membership.Abstractions.Interfaces.Login;
public interface ILoginOutputPort
{
    UserTokensDTO UserTokens { get; }
    Task HandleUserEntityAsync(UserEntity userEntity);
}
