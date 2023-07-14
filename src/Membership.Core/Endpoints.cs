namespace Membership.Core;
public static class Endpoints
{
    public static WebApplication UseMembershipEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", async (IRegisterController controller, UserDTO user) =>
        {
            await controller.RegisterAsync(user);
            return Results.Ok();
        });

        app.MapPost("/user/login", async (ILoginController controller,
                UserCredentialsDTO userCredentials, HttpContext context) =>
        {
            context.Response.Headers.Add("Cache-Control", "no-store");
            var result = await controller.LogingAsync(userCredentials);
            return Results.Ok(result);
        });

        app.MapPost("/user/logout", async (ILogoutController controller,
            UserTokensDTO userTokens) =>
        {
            await controller.LogoutAsycn(userTokens);
            return Results.Ok();
        });

        app.MapPost("/user/refreshtoken", async (IRefreshTokenController controller,
                       UserTokensDTO userTokens, HttpContext context) =>
        {
            context.Response.Headers.Add("Cache-Control", "no-store");
            var result = await controller.RefreshTokenAsync(userTokens);
            return Results.Ok(result);
        });

        return app;
    }
}
