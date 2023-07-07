namespace Membership.Core.Interactors;
internal class LoginInteractor : ILoginInputPort
{
    readonly IUserManagerService _userManagerService;
    readonly IValidator<UserCredentialsDTO> _validator;
    readonly ILoginOutputPort _outputPort;

    public LoginInteractor(IUserManagerService userManagerService, IValidator<UserCredentialsDTO> validator, ILoginOutputPort outputPort)
    {
        _userManagerService = userManagerService;
        _validator = validator;
        _outputPort = outputPort;
    }


    public async Task LoginAsync(UserCredentialsDTO userCredentials)
    {
        var validationErrors = _validator.Validate(userCredentials);
        if (validationErrors != null && validationErrors.Any())
        {
            throw new LoginUserException();
        }

        var user = await _userManagerService
            .ThrowIfUnableToGetUserByCredentialsAsync(userCredentials);

        await _outputPort.HandleUserEntityAsync(user);
    }
}
