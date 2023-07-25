using Membership.Shared.ValueObjects;

namespace Membership.Shared.Validators;
public class UserCredentialsDTOValidator : AbstractValidator<UserCredentialsDTO>
{
    public UserCredentialsDTOValidator(IMembershipMessageLocalizer localizer) : base(localizer)
    {
    }

    protected override void ValidatePropertyRules(UserCredentialsDTO entity, string propertyName, List<MembershipError> errors)
    {
        switch (propertyName)
        {
            case nameof(UserCredentialsDTO.Email):
                ValidateRule(() => !string.IsNullOrWhiteSpace(entity.Email),
                                       propertyName, MessageKeys.RequiredEmailErrorMessage, errors);
                break;
            case nameof(UserCredentialsDTO.Password):
                ValidateRule(() => !string.IsNullOrWhiteSpace(entity.Password),
                                       propertyName, MessageKeys.RequiredPasswordErrorMessage, errors);
                break;
        }
    }
}
