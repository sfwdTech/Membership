using Microsoft.AspNetCore.Builder;

namespace Membership.Core;
public static class Endpoints
{
    public static WebApplication UseMembershipEndpoints(this WebApplication app)
    {


        return app;
    }
}
