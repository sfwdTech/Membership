namespace Membership.Shared.Constants;
public class MessageKeys
{
    public const string RequiredFirstNameErrorMessage = nameof(RequiredFirstNameErrorMessage);
    public const string RequiredLastNameErrorMessage = nameof(RequiredLastNameErrorMessage);
    public const string RequiredEmailErrorMessage = nameof(RequiredEmailErrorMessage);
    public const string RequiredPasswordErrorMessage = nameof(RequiredPasswordErrorMessage);
    public const string CompareConfirmPasswordErrorMessage = nameof(CompareConfirmPasswordErrorMessage);

    public const string DisplayFirstNameMessage = nameof(DisplayFirstNameMessage);
    public const string DisplayLastNameMessage = nameof(DisplayLastNameMessage);
    public const string DisplayEmailMessage = nameof(DisplayEmailMessage);
    public const string DisplayPasswordMessage = nameof(DisplayPasswordMessage);
    public const string DisplayConfirmPasswordMessage = nameof(DisplayConfirmPasswordMessage);
    public const string DisplayRegisterButtonMessage = nameof(DisplayRegisterButtonMessage);
    public const string DisplayLoginButtonMessage = nameof(DisplayLoginButtonMessage);
    public const string DisplayLogoutButtonMessage = nameof(DisplayLogoutButtonMessage);

    public const string PasswordRequiresDigitErrorMessage = nameof(PasswordRequiresDigitErrorMessage);
    public const string PasswordRequiresNonAlphanumericErrorMessage = nameof(PasswordRequiresNonAlphanumericErrorMessage);
    public const string PasswordRequiresUpperErrorMessage = nameof(PasswordRequiresUpperErrorMessage);
    public const string PasswordRequiresLowerErrorMessage = nameof(PasswordRequiresLowerErrorMessage);
    public const string PasswordToShortErrorMessage = nameof(PasswordToShortErrorMessage);

    public const string DuplicateEmailErrorMessage = nameof(DuplicateEmailErrorMessage);
    public const string LoginAlreadyAssociatedErrorMessage = nameof(LoginAlreadyAssociatedErrorMessage);

    public const string RegisterUserExceptionMessage = nameof(RegisterUserExceptionMessage);
    public const string LoginUserExceptionMessage = nameof(LoginUserExceptionMessage);
    public const string RefreshTokenCompromisedExceptionMessage = nameof(RefreshTokenCompromisedExceptionMessage);
    public const string RefreshTokenExpiredExceptionMessage = nameof(RefreshTokenExpiredExceptionMessage);
    public const string RefreshTokenNotFoundExceptionMessage = nameof(RefreshTokenNotFoundExceptionMessage);
}
