using System.Globalization;

namespace Membership.Shared.MessageLocalizer;
public class MembershipMessageLocalizer : IMembershipMessageLocalizer
{
    readonly Dictionary<string, string> _messages = new()
    {
        {MessageKeys.RequiredFirstNameErrorMessage, "El nombre es requerido." },
        {MessageKeys.RequiredLastNameErrorMessage, "El apellido es requerido." },
        {MessageKeys.RequiredEmailErrorMessage, "El correo es requerido." },
        {MessageKeys.RequiredPasswordErrorMessage, "La contraseña es requerida." },
        {MessageKeys.CompareConfirmPasswordErrorMessage, "La contraseña y la confirmación no coinciden." },
        {MessageKeys.PasswordRequiresDigitErrorMessage, "Se requiere al menos un dígito en la contraseña." },
        {MessageKeys.PasswordRequiresLowerErrorMessage,
            "Se requiere al menos un caracter minúscula en la contraseña." },
        {MessageKeys.PasswordRequiresNonAlphanumericErrorMessage,
            "Se requiere al menos un caracter no alfanumérico en la contraseña." },
        {MessageKeys.PasswordRequiresUpperErrorMessage,
            "Se requiere al menos un caracter mayúscula en la contraseña." },
        {MessageKeys.PasswordToShortErrorMessage,
            "Se requiere al menos 6 caracteres en la contraseña." },

        {MessageKeys.DuplicateEmailErrorMessage,
            "El correo proporcionado ya se encuentra registrado." },
        {MessageKeys.LoginAlreadyAssociatedErrorMessage,
            "El correo proporcionado ya se encuentra asociado a una cuenta de usuario." },

        {MessageKeys.DisplayFirstNameMessage, "Nombre:" },
        {MessageKeys.DisplayLastNameMessage, "Apellidos:" },
        {MessageKeys.DisplayEmailMessage, "Correo:" },
        {MessageKeys.DisplayPasswordMessage, "Contraseña:" },
        {MessageKeys.DisplayConfirmPasswordMessage, "Confirmar contraseña:" },
        {MessageKeys.DisplayRegisterButtonMessage, "Registrar" },
        {MessageKeys.DisplayLoginButtonMessage, "Iniciar sesión" },
        {MessageKeys.DisplayLogoutButtonMessage, "Cerrar sesión" },

        {MessageKeys.RegisterUserExceptionMessage,
            "Error al registrar al usuario." },
        {MessageKeys.LoginUserExceptionMessage,
            "Las credenciales proporcionadas son incorrectas." },
        {MessageKeys.RefreshTokenCompromisedExceptionMessage,
            "El token de actualización fue comprometido."},
        {MessageKeys.RefreshTokenExpiredExceptionMessage,
            "El token de actualización ha expirado."},
        {MessageKeys.RefreshTokenNotFoundExceptionMessage,
            "El token de actualización no fue encontrado."}
    };

    //readonly Dictionary<string, string> _messages_en = new()
    //{
    //    {MessageKeys.RequiredFirstNameErrorMessage, "The FirstName is required."},
    //    {MessageKeys.RequiredLastNameErrorMessage, "The LastName is required."},
    //    {MessageKeys.RequiredEmailErrorMessage, "The email is required."},
    //    {MessageKeys.RequiredPasswordErrorMessage, "The password is required."},
    //    {MessageKeys.DisplayConfirmPasswordMessage, "Password confirmation is required."},
    //    {MessageKeys.PasswordToShortErrorMessage, "The password must be at least 6 characters."},
    //    {MessageKeys.CompareConfirmPasswordErrorMessage, "Passwords do not match."},
    //    {MessageKeys.RegisterUserExceptionMessage, "Error registering user."},
    //    {MessageKeys.LoginUserExceptionMessage, "The credentials provided are incorrect."},
    //    {MessageKeys.RefreshTokenCompromisedExceptionMessage, "The refresh token has been compromised."},
    //    {MessageKeys.RefreshTokenExpiredExceptionMessage, "The refresh token has expired."},
    //    {MessageKeys.RefreshTokenNotFoundExceptionMessage, "The refresh token was not found."},
    //};

    public string this[string key]
    {
        get
        {
            //CultureInfo culture = CultureInfo.CurrentCulture;
            //Dictionary<string, string> messages;

            //if (culture.Name == "es-ES")
            //{
            //    messages = _messages_es;
            //}
            //else
            //{
            //    messages = _messages_en;
            //}

            _messages.TryGetValue(key, out var _message);
            return string.IsNullOrWhiteSpace(_message) ? key : _message;
        }
    }
}
