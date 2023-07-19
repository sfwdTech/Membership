using Membership.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages;

public partial class Login
{
    const string RouteTemplate = "user/login";

    [Inject]
    NavigationManager NavigationManager { get; set; }

    void OnLogin(UserTokensDTO tokens)
    {
        string NavigateTo = NavigationManager.Uri.EndsWith(RouteTemplate) ? "" : NavigationManager.Uri;

        NavigationManager.NavigateTo(NavigateTo);
    }
}