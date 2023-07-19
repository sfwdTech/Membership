namespace Membership.Blazor.Components;

public partial class LogoutComponent
{
    [Inject]
    IUserWebApiGateway UserWebApiGateway { get; set; }
    [Inject]
    IAuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject]
    IMembershipMessageLocalizer Localizer { get; set; }

    [Inject]
    NavigationManager NavigationManager { get; set; }

    async void Logout()
    {
        var storedTodkens = await AuthenticationStateProvider.GetUserTokensAsync();
        if (storedTodkens != null)
        {
            await UserWebApiGateway.LogoutAsync(storedTodkens);
        }
        await AuthenticationStateProvider.LogoutAsync();
        NavigationManager.NavigateTo("");
    }
}