namespace Membership.Core.Services;
internal class UserService : IUserService
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => 
        _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

    public string Email => _httpContextAccessor.HttpContext.User.Identity.Name;
    public string FullName => _httpContextAccessor.HttpContext.User.Claims
        .Where(c => c.Type == "FullName")
        .Select(c => c.Value).FirstOrDefault();
}
