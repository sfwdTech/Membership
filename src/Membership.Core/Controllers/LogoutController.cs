namespace Membership.Core.Controllers;
internal class LogoutController 
{
    public static void Map(WebApplication app)
    {
        app.MapPost(MembershipEndpoints.Logout, async (ILogoutInputPort inputPort,
            UserTokensDTO userTokens) =>
        {
            await inputPort.LogoutAsync(userTokens);
            return Results.Ok();
        });
    }
}
