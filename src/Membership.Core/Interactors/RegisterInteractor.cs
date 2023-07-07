namespace Membership.Core.Interactors;
internal class RegisterInteractor : IRegisterInputPort
{
    readonly IUserManagerService _userManagerService;
    readonly IValidator<UserDTO> _userValidator;

    public RegisterInteractor(IUserManagerService userManagerService, IValidator<UserDTO> validator)
    {
        _userManagerService = userManagerService;
        _userValidator = validator;
    }

    public async Task RegisterAsync(UserDTO user)
    {
        var validationErrors = _userValidator.Validate(user);

        if (validationErrors != null && validationErrors.Any())
        {
            throw new RegisterUserException(validationErrors);
        }

        await _userManagerService.ThrowIfUnableToRegisterUserAsync(user);

    }
}
