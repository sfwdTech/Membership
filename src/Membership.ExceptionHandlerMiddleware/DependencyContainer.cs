using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.ExceptionHandlerMiddleware;
public static class DependencyContainer
{
    public static IApplicationBuilder UseMembershipExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async (context) =>
            await MembershipExceptionHandler.WriteResponse(context,
            app.ApplicationServices.GetRequiredService<IMembershipMessageLocalizer>()!));
        });

        return app;
    }
}
