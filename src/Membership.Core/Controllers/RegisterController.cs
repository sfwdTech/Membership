namespace Membership.Core.Controllers;
public class RegisterController 
{ 
    public static void Map(WebApplication app)
    {
        app.MapPost(MembershipEndpoints.Register, async (UserDTO user, IRegisterInputPort inputPort) =>
        {
            await inputPort.RegisterAsync(user);
            return Results.Ok();
        });
    }
}
