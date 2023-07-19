using Membership.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class Register
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        void OnRegister(UserDTO user)
        {
            Console.WriteLine($"Usuario Registrado; {user.FirstName}  {user.LastName}");
            NavigationManager.NavigateTo("user/login");
        }
    }
}