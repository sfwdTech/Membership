namespace Membership.RefreshTokenService.MemoryCache;
internal class RefreshTokenInfo
{
    public string AccessToken { get; }
    public DateTime RefreshToeknExpiresAt { get; }

    public RefreshTokenInfo(string accessToken, DateTime refreshToeknExpiresAt)
    {
        AccessToken = accessToken;
        RefreshToeknExpiresAt = refreshToeknExpiresAt;
    }
}
