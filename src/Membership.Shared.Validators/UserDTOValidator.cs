namespace Membership.Shared.Validators;
public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator(IMembershipMessageLocalizer localizer) : base(localizer)
    {
    }

    protected override void ValidatePropertyRules(UserDTO entity, string propertyName, List<MembershipError> errors)
    {
        switch (propertyName)
        {
            case nameof(UserDTO.FirstName):
                ValidateRule(() => !string.IsNullOrWhiteSpace(entity.FirstName),
                    propertyName, MessageKeys.RequiredFirstNameErrorMessage, errors);
                break;
            case nameof(UserDTO.LastName):
                ValidateRule(() => !string.IsNullOrWhiteSpace(entity.LastName),
                     propertyName, MessageKeys.RequiredLastNameErrorMessage, errors);
                break;
            case nameof(UserDTO.Email):
                ValidateRule(() => !string.IsNullOrWhiteSpace(entity.LastName),
                     propertyName, MessageKeys.RequiredEmailErrorMessage, errors);
                break;
            case nameof(UserDTO.Password):
                if (ValidateRule(() => 
                    !string.IsNullOrWhiteSpace(entity.Password), 
                    propertyName, MessageKeys.RequiredPasswordErrorMessage, errors))
                {
                    ValidateRule(() => entity.Password.Length > 6, propertyName, MessageKeys.PasswordToShortErrorMessage, errors);
                    ValidateRule(() => entity.Password.Any(c => char.IsLower(c)), propertyName, MessageKeys.PasswordRequiresLowerErrorMessage, errors);
                    ValidateRule(() => entity.Password.Any(c => char.IsUpper(c)), propertyName, MessageKeys.PasswordRequiresUpperErrorMessage, errors);
                    ValidateRule(() => entity.Password.Any(c => char.IsDigit(c)), propertyName, MessageKeys.PasswordRequiresDigitErrorMessage, errors);
                    ValidateRule(() => entity.Password.Any(c => !char.IsLetterOrDigit(c)), propertyName, MessageKeys.PasswordRequiresNonAlphanumericErrorMessage, errors);
                }
                break;
            case nameof(UserDTO.ConfirmPassword):
                    ValidateRule(() => entity.Password == entity.ConfirmPassword, propertyName, MessageKeys.CompareConfirmPasswordErrorMessage, errors);
                break;
        }
    }
}
