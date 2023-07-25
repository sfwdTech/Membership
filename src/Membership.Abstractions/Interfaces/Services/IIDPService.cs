namespace Membership.Abstractions.Interfaces.Services;
public interface IIDPService
{
    Task<string> GetAuthorizeRequestUri(string providerId, string state, string codeVerifier, string nonce);

    Task<IDPTokens> GetTokesAsync(string providerId, string authorizeCode, string codeVerifier, string nonce);
}
