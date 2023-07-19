using Membership.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Blazor.Components;
public partial class RegisterComponent
{
    [Inject]
    IUserWebApiGateway UserWebApiGateway { get; set; }

    [Inject]
    IValidator<UserDTO> UserValidator { get; set; }

    [Inject]
    IMembershipMessageLocalizer Localizer { get; set; }

    [Parameter]
    public EventCallback<UserDTO> OnRegister { get; set;}

    MembershipValidator<UserDTO> _membershipValidator;

    UserDTO _user = new();

    async Task Register()
    {
        try
        {
            await UserWebApiGateway.RegisterUserAsync(_user);
            await OnRegister.InvokeAsync(_user);
        }
        catch (HttpRequestException ex)
        {
            _membershipValidator.TrySetErrorsFromHttpRequestException(ex);
        }
        catch 
        {
            throw;
        }
    }
}
