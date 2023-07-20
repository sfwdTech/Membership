namespace Membership.Core.Controllers;
internal class LoginController 
{
    public static void Map(WebApplication app)
    {
        app.MapPost(MembershipEndpoints.Login, async (HttpContext context, 
            UserCredentialsDTO userCredentials, ILoginInputPort inputPort, ILoginOutputPort outputPort) =>
        {
            context.Response.Headers.Add("Cache-Control", "no-store");
            await inputPort.LoginAsync(userCredentials);
            return Results.Ok(outputPort.UserTokens);
        });
    }
}
