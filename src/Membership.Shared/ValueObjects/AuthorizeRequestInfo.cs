namespace Membership.Shared.ValueObjects;
public class AuthorizeRequestInfo
{
    public string AuthorizeEndpoint { get; set; }
    public string ClientId { get; set; }
    public string RedirectUri { get; set; }
    public string State { get; set; }
    public string Scope { get; set; }
    public string CodeChallenge { get; set; }
    public string CodeChallengeMethod { get; set; }
    public string Nonce { get; set; }

    public AuthorizeRequestInfo(string authorizeEndpoint, string clientId, string redirectUri,
        string state, string scope, string codeChallenge, string codeChallengeMethod, string nonce)
    {
        AuthorizeEndpoint = authorizeEndpoint;
        ClientId = clientId;
        RedirectUri = redirectUri;
        State = state;
        Scope = scope;
        CodeChallenge = codeChallenge;
        CodeChallengeMethod = codeChallengeMethod;
        Nonce = nonce;
    }
}
