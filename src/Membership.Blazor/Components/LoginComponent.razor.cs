using Membership.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Membership.Blazor.Components
{
    public partial class LoginComponent
    {
        [Inject]
        IUserWebApiGateway UserWebApiGateway { get; set; }
        [Inject]
        IAuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        IValidator<UserCredentialsDTO> UserCredentialsValidator { get; set; }
        [Inject]
        IMembershipMessageLocalizer Localizer { get; set; }

        [Parameter]
        public EventCallback<UserTokensDTO> OnLogin { get; set; }

        UserCredentialsDTO _userCredentials = new();

        string _errorMessage;

        async Task Login()
        {
            try
            {
                _errorMessage = null;
                var userTokens = await UserWebApiGateway.LoginAsync(_userCredentials);
                await AuthenticationStateProvider.LoginAsync(userTokens);
                await OnLogin.InvokeAsync(userTokens);
            }
            catch(HttpRequestException ex)
            {
                _errorMessage = ex.Message;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}