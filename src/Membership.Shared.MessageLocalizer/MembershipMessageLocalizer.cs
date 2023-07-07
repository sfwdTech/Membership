using System.Globalization;

namespace Membership.Shared.MessageLocalizer;
public class MembershipMessageLocalizer : IMembershipMessageLocalizer
{
    readonly Dictionary<string, string> _messages_es = new()
    {
        {MessageKeys.RequiredFirstNameErrorMessage, "El nombre es requerido."},
        {MessageKeys.RequiredLastNameErrorMessage, "El apellido es requerido."},
        {MessageKeys.RequiredEmailErrorMessage, "El correo electrónico es requerido."},
        {MessageKeys.RequiredPasswordErrorMessage, "La contraseña es requerida."},
        {MessageKeys.DisplayConfirmPasswordMessage, "La confirmación de la contraseña es requerida."},
        {MessageKeys.PasswordToShortErrorMessage, "La contraseña debe tener al menos 6 caracteres."},
        {MessageKeys.CompareConfirmPasswordErrorMessage, "Las contraseñas no coinciden."},
    };

    readonly Dictionary<string, string> _messages_en = new()
    {
        {MessageKeys.RequiredFirstNameErrorMessage, "The FirstName is required."},
        {MessageKeys.RequiredLastNameErrorMessage, "The LastName is required."},
        {MessageKeys.RequiredEmailErrorMessage, "The email is required."},
        {MessageKeys.RequiredPasswordErrorMessage, "The password is required."},
        {MessageKeys.DisplayConfirmPasswordMessage, "Password confirmation is required."},
        {MessageKeys.PasswordToShortErrorMessage, "The password must be at least 6 characters."},
        {MessageKeys.CompareConfirmPasswordErrorMessage, "Passwords do not match."},
    };

    public string this[string key]
    {
        get
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            Dictionary<string, string> messages;

            if (culture.Name == "es-ES")
            {
                messages = _messages_es;
            }
            else
            {
                messages = _messages_en;
            }

            messages.TryGetValue(key, out var message);
            return string.IsNullOrWhiteSpace(message) ? key : message;
        }
    }
}
