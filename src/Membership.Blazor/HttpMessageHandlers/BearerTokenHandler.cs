namespace Membership.Blazor.HttpMessageHandlers;
internal class BearerTokenHandler : DelegatingHandler
{
    readonly IAuthenticationStateProvider _authenticationStateProvider;

    public BearerTokenHandler(IAuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var storedTokens = await _authenticationStateProvider.GetUserTokensAsync();
        if (storedTokens != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",
                storedTokens.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
