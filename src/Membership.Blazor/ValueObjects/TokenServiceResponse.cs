namespace Membership.Blazor.ValueObjects;
internal class TokenServiceResponse
{
    public UserTokensDTO Tokens { get; set; }
    public string Scope { get; set; }
    public string ReturnUri { get; set; }
}
