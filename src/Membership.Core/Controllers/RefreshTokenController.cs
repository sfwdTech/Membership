namespace Membership.Core.Controllers;
internal class RefreshTokenController 
{
    public static void Map(WebApplication app)
    {
        app.MapPost(MembershipEndpoints.RefreshToken, async (HttpContext context, UserTokensDTO userTokens,
            IRefreshTokenInputPort inputPort, IRefreshTokenOutputPort outPutPort) =>
        {
            context.Response.Headers.Add("Cache-Control", "no-store");
            await inputPort.RefreshTokenAsync(userTokens);
            return Results.Ok(outPutPort.UserTokens);
        });
    }
}
