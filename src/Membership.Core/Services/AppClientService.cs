namespace Membership.Core.Services;
internal class AppClientService : IAppClientService
{
    readonly AppClientInfoOptions _appClientInfo;

    public AppClientService(IOptions<AppClientInfoOptions> appClientInfo)
    {
        _appClientInfo = appClientInfo.Value;
    }


    public void ThrowIfNotExist(string clientId, string redirectUri)
    {
        if(!_appClientInfo.AppClients.Any(c => c.ClientId == clientId && c.RedirectUri == redirectUri))
            throw new UnauthorizedAccessException();
    }
}
