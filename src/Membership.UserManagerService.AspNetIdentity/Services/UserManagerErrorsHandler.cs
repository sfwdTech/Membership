namespace Membership.UserManagerService.AspNetIdentity.Services;
internal class UserManagerErrorsHandler
{
    readonly IMembershipMessageLocalizer _localizer;

    public UserManagerErrorsHandler(IMembershipMessageLocalizer localizer)
    {
        _localizer = localizer;
    }

    public IEnumerable<MembershipError> Handle(IEnumerable<IdentityError> errors)
    {
        List<MembershipError> errorsList = new List<MembershipError>();
        foreach (var error in errors)
        {
            switch (error.Code)
            {
                case nameof(IdentityErrorDescriber.DuplicateUserName):
                    errorsList.Add(new MembershipError(
                        nameof(User.Email), _localizer[MessageKeys.DuplicateEmailErrorMessage]));
                    break;
                case nameof(IdentityErrorDescriber.LoginAlreadyAssociated):
                    errorsList.Add(new MembershipError(
                                   nameof(User.Email), _localizer[MessageKeys.LoginAlreadyAssociatedErrorMessage]));
                    break;
                default:
                    errorsList.Add(new MembershipError(error.Code, error.Description));
                    break;
            }
        }
        return errorsList;
    }
}
