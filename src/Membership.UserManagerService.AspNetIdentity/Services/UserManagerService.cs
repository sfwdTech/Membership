namespace Membership.UserManagerService.AspNetIdentity.Services;
internal partial class UserManagerService : IUserManagerService
{
    readonly UserManagerErrorsHandler _errorsHandler;
    readonly UserManager<User> _userManager;

    public UserManagerService(
               UserManager<User> userManager,
               UserManagerErrorsHandler errorsHandler)
    {
        _userManager = userManager;
        _errorsHandler = errorsHandler;
    }
}
